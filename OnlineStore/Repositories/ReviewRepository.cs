using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext dbContext;

        public ReviewRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<int> CountByProductIdAsync(int id)
        {
            return dbContext.Reviews.Where(r => r.ProductId == id).CountAsync();
        }

        public Task<List<Review>> GetByProductIdAsync(int id, int pageNumber, int pageSize)
        {
            return dbContext.Reviews
                .Where(r => r.ProductId == id)
                .OrderByDescending(r => r.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(r => r.User)
                .ToListAsync();
        }

        public Task AddAsync(Review review)
        {
            dbContext.Reviews.Add(review);
            return dbContext.SaveChangesAsync();
        }
    }
}
