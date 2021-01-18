using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> AddAsync(Order order)
        {
            dbContext.Orders.Add(order);

            await dbContext.SaveChangesAsync();

            return order.Id;
        }

        public Task<Order> GetUncompletedOrderAsync(int userId, int cartId)
        {
            return dbContext.Orders
                .Where(o => o.Completed == false && o.UserId == userId && o.CartId == cartId)
                .FirstOrDefaultAsync();
        }

        public Task UpdateAsync(Order order)
        {
            dbContext.Orders.Update(order);

            return dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Order order)
        {
            dbContext.Orders.Remove(order);

            return dbContext.SaveChangesAsync();
        }

        public Task<bool> CartIdExistsInOrdersAsync(int cartId)
        {
            return dbContext.Orders.Where(o => o.CartId == cartId).AnyAsync();
        }

        public Task<List<Order>> GetOrderListWithProductsAsync(int userId, int page, int pageSize)
        {
            return dbContext.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.Date)
                .Include(o => o.OrderProducts)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<int> CountOrdersAsync(int userId)
        {
            return dbContext.Orders
                .Where(o => o.UserId == userId)
                .CountAsync();
        }
    }
}
