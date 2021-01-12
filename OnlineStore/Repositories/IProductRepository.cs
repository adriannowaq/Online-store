using OnlineStore.Data;
using OnlineStore.Models.Account.Admin;
using OnlineStore.Models.Shop;
using OnlineStore.Repositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(AddProductModel productDetails);
        Task<ProductModel> FindByIdAsync(int id);
        Task<List<SearchProductsModel>> SearchProductsByLettersAsync(string letters, int limit = 10);
        Task<List<Product>> GetProductsPerPageAsync(int? category, int page, int itemsCount, int order);
        Task<int> CountProductsAsync(int? category);
    }
}
