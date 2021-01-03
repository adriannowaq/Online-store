using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly AppDbContext dbContext;

        public ProductCategoryRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task AddCategoryAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            return dbContext.ProductsCategories.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
