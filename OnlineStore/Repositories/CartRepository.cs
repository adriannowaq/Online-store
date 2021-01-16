using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext dbContext;
        private readonly ITokenHelper tokenHelper;

        public CartRepository(AppDbContext dbContext, ITokenHelper tokenHelper)
        {
            this.dbContext = dbContext;
            this.tokenHelper = tokenHelper;
        }

        public async Task<Cart> FindByTokenAsync(string token)
        {
            var cart = await dbContext.Carts
                        .Where(c => c.Token.Equals(token))
                        .Include(c => c.CartItems)
                            .ThenInclude(c => c.Product)
                        .Include(c => c.User)
                        .FirstOrDefaultAsync();

            if (cart != null)
            {
                cart.CartItems.ForEach(c => c.Cost = c.Product.Price * c.Count);
                cart.SummaryCost = cart.CartItems.Sum(c => c.Cost);
            }

            return cart;
        }

        public async Task<Cart> CreateCartAsync()
        {
            var token = tokenHelper.GenerateToken();
            var cart = new Cart { Token = token };

            dbContext.Carts.Add(cart);
            await dbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> AddItemToCartAsync(Cart cart, int productId, byte count)
        {
            var cartItem = await dbContext.CartItems
                .Where(ci => ci.CartId == cart.Id && ci.ProductId == productId)
                .FirstOrDefaultAsync();

            if (cartItem == null)
            {
                dbContext.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Count = count
                });
            }
            else
            {
                cartItem.Count += count;
                dbContext.CartItems.Update(cartItem);
            }
            await dbContext.SaveChangesAsync();

            return await FindByTokenAsync(cart.Token);
        }

        public async Task<Cart> RemoveItemFromCartAsync(Cart cart, int productId, byte count)
        {
            var cartItem = await dbContext.CartItems
                .Where(ci => ci.CartId == cart.Id && ci.ProductId == productId)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                if (cartItem.Count <= count)
                {
                    dbContext.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Count -= count;
                    dbContext.CartItems.Update(cartItem);
                }
                await dbContext.SaveChangesAsync();

                return await FindByTokenAsync(cart.Token);
            }
            return cart;
        }

        public async Task<Cart> DeleteItemFromCartAsync(Cart cart, int productId)
        {
            var cartItem = await dbContext.CartItems
                .Where(ci => ci.CartId == cart.Id && ci.ProductId == productId)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                dbContext.CartItems.Remove(cartItem);
                await dbContext.SaveChangesAsync();

                return await FindByTokenAsync(cart.Token);
            }
            return cart;
        }

        public async Task<Cart> UpdateItemInCartAsync(Cart cart, int productId, byte count)
        {
            var cartItem = await dbContext.CartItems
                .Where(ci => ci.CartId == cart.Id && ci.ProductId == productId)
                .FirstOrDefaultAsync();

            if (cartItem != null)
            {
                cartItem.Count = count;
                dbContext.CartItems.Update(cartItem);
                await dbContext.SaveChangesAsync();

                return await FindByTokenAsync(cart.Token);
            }
            return cart;
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            dbContext.Carts.Update(cart);
            await dbContext.SaveChangesAsync();

            return await FindByTokenAsync(cart.Token);
        }

        public Task DeleteCartAsync(Cart cart)
        {
            dbContext.Carts.Remove(cart);

            return dbContext.SaveChangesAsync();
        }
    }
}
