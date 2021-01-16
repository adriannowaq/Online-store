using OnlineStore.Data;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> FindByTokenAsync(string token);
        Task<Cart> CreateCartAsync();

        /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Thrown when productId doesn't exist in database.</exception>
        Task<Cart> AddItemToCartAsync(Cart cart, int productId, byte count);
        Task<Cart> RemoveItemFromCartAsync(Cart cart, int productId, byte count);
        Task<Cart> DeleteItemFromCartAsync(Cart cart, int productId);
        Task<Cart> UpdateCartAsync(Cart cart);
        Task<Cart> UpdateItemInCartAsync(Cart cart, int productId, byte count);
        Task DeleteCartAsync(Cart cart);
    }
}
