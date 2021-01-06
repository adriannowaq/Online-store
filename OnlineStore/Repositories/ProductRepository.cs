using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Controllers;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Extensions;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Models.Account.Admin;
using OnlineStore.Models.Shop;
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
        private readonly LinkGenerator linkGenerator;

        public ProductRepository(AppDbContext dbContext, 
                                ICloudStorageService cloudStorageService,
                                IHtmlSanitizationService htmlSanitizationService,
                                LinkGenerator linkGenerator)
        {
            this.dbContext = dbContext;
            this.cloudStorageService = cloudStorageService;
            this.htmlSanitizationService = htmlSanitizationService;
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

        public async Task<List<SearchProductsModel>> SearchProductsByLettersAsync(string letters, int limit = 10)
        {
            return (await dbContext.Products
                .Where(p => p.Name.Contains(letters, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(p => p.Name)
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
    }
}
