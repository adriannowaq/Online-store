using OnlineStore.Models.Account.Admin;
using OnlineStore.Models.Shop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(AddProductModel productDetails);
        Task<List<SearchProductsModel>> SearchProductsByLettersAsync(string letters, int limit = 10);
    }
}
