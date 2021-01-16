using OnlineStore.Data;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync();

        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Thrown when productId doesn't exist in database.</exception>
        Task<Cart> AddItemToCartAsync(Cart cart, int productId, byte count);
        Task<Cart> DeleteItemFromCartAsync(Cart cart, int productId);
        Task<Cart> UpdateItemInCartAsync(Cart cart, int productId, byte count);
        Task DeleteCartAsync(Cart cart);
    }
}
