using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Controllers;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Extensions;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Models.Account.Admin;
using OnlineStore.Models.Shop;
using OnlineStore.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;
        private readonly ICloudStorageService cloudStorageService;
        private readonly IHtmlSanitizationService htmlSanitizationService;
        private readonly IReviewRepository reviewRepository;
        private readonly LinkGenerator linkGenerator;

        public ProductRepository(AppDbContext dbContext,
                                ICloudStorageService cloudStorageService,
                                IHtmlSanitizationService htmlSanitizationService,
                                LinkGenerator linkGenerator,
                                IReviewRepository reviewRepository)
        {
            this.dbContext = dbContext;
            this.cloudStorageService = cloudStorageService;
            this.htmlSanitizationService = htmlSanitizationService;
            this.reviewRepository = reviewRepository;
            this.linkGenerator = linkGenerator;
        }

        public async Task AddAsync(AddProductModel productDetails)
        {
            var sanitizeHtml = htmlSanitizationService.SanitizeData(productDetails.Description);
            var cloudResult = await cloudStorageService.UploadFileAsync(productDetails.ImageFile);
            var product = new Product()
            {
                Name = productDetails.Name,
                Producer = productDetails.Producer,
                Price = productDetails.Price,
                Description = sanitizeHtml,
                CloudStorageImageName = cloudResult.FileName,
                CloudStorageImageUrl = cloudResult.FileUrl,
                Count = productDetails.Count,
                ProductCategoryId = productDetails.ProductCategory
            };

            await dbContext.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public Task<int> CountProductsAsync(int? category)
        {
            if (category != null)
                return dbContext.Products.Where(p => p.ProductCategoryId == category).CountAsync();

            return dbContext.Products.CountAsync();
        }

        public Task<List<Product>> GetProductsPerPageAsync(int? category, 
            int page, int pageSize, int order)
        {
            IQueryable<Product> products = dbContext.Products;

            if (order == 1)
            {
                products = products.OrderBy(p=>p.Price);
            }
            else if (order == 2)
            {
                products = products.OrderByDescending(p => p.Price);
            }

            if (category != null)
                products = products.Where(p => p.ProductCategoryId == category);


            return products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<List<SearchProductsModel>> SearchProductsByLettersAsync(string letters, 
            int limit = 10)
        {
            return (await dbContext.Products
                .Where(p => p.Name.Contains(letters, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.Name)
                .Take(limit)
                .Select(p => new SearchProductsModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync())
                .Select(p => new SearchProductsModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Url = linkGenerator.GetPathByAction(nameof(ShopController.Details), 
                        nameof(ShopController).RemoveController(), new { p.Id })
                })
                .ToList();
        }

        public Task<ProductModel> FindByIdAsync(int id)
        {
            return (from p in dbContext.Products
                    where p.Id == id
                    let count = dbContext.Reviews.Where(r => r.ProductId == id).Count()
                    let average = dbContext.Reviews.Where(r => r.ProductId == id).Select(r => r.Rate).Average()
                    select new ProductModel
                    {
                        Product = p,
                        CountAll = count,
                        AverageRate = average
                    }).FirstOrDefaultAsync();
        }

        public Task<List<Product>> GetMostPopularProductsAsync()
        {
            return dbContext.Products.Take(8).ToListAsync();
        }
    }
}
