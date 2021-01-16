using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
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
    }
}
