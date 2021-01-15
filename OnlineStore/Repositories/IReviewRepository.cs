using OnlineStore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IReviewRepository
    {
        Task<int> CountByProductIdAsync(int id);
        Task<List<Review>> GetByProductIdAsync(int id, int pageNumber, int pageSize);
        Task<List<Review>> GetLatestReviewsAsync(int limit);
        Task AddAsync(Review review);
    }
}
