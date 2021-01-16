using Microsoft.AspNetCore.Http;
using OnlineStore.Data;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ICartRepository cartRepository;
        private string TokenShopCartName = "ShopCart";

        public CartService(IHttpContextAccessor httpContextAccessor, 
                           ICartRepository cartRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.cartRepository = cartRepository;
        }

        public async Task<Cart> GetCartAsync()
        {
            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(TokenShopCartName))
            {
                var cartToken = httpContextAccessor.HttpContext.Request.Cookies[TokenShopCartName];
                var cart = await cartRepository.FindByTokenAsync(cartToken);
                if (cart != null)
                    return cart;
            }
            var createdCart = await cartRepository.CreateCartAsync();
            httpContextAccessor.HttpContext.Response.Cookies.Append(TokenShopCartName, createdCart.Token);

            return createdCart;
        }

        public Task DeleteCartAsync(Cart cart)
        {
            httpContextAccessor.HttpContext.Response.Cookies.Delete(TokenShopCartName);

            return cartRepository.DeleteCartAsync(cart);
        }

        /// <exception cref="DbUpdateException">Thrown when productId doesn't exist in database.</exception>
        public Task<Cart> AddItemToCartAsync(Cart cart, int productId, byte count) =>
            cartRepository.AddItemToCartAsync(cart, productId, count);

        public Task<Cart> DeleteItemFromCartAsync(Cart cart, int productId) =>
            cartRepository.DeleteItemFromCartAsync(cart, productId);

        public Task<Cart> UpdateItemInCartAsync(Cart cart, int productId, byte count) =>
            cartRepository.UpdateItemInCartAsync(cart, productId, count);
    }
}
