using OnlineStore.Data;
using OnlineStore.Infrastructure.Services;
using OnlineStore.Models.Account.Admin;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;
        private readonly ICloudStorageService cloudStorageService;
        private readonly IHtmlSanitizationService htmlSanitizationService;

        public ProductRepository(AppDbContext dbContext, 
                                ICloudStorageService cloudStorageService,
                                IHtmlSanitizationService htmlSanitizationService)
        {
            this.dbContext = dbContext;
            this.cloudStorageService = cloudStorageService;
            this.htmlSanitizationService = htmlSanitizationService;
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
    }
}
