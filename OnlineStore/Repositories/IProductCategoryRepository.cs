using OnlineStore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IProductCategoryRepository
    {
        Task<List<ProductCategory>> GetProductCategoriesAsync();
        Task AddCategoryAsync();
    }
}
