using OnlineStore.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface IOrderRepository
    {
        Task<int> AddAsync(Order order);
        Task<Order> GetUncompletedOrderAsync(int userId, int cartId);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task<bool> CartIdExistsInOrdersAsync(int cartId);
        Task<List<Order>> GetOrderListWithProductsAsync(int userId, int page, int pageSize);
        Task<int> CountOrdersAsync(int userId);
    }
}
