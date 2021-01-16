using Microsoft.AspNetCore.Mvc;
using OnlineStore.Infrastructure.Services;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService cartService;

        public CartSummaryViewComponent(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await cartService.GetCartAsync());
        }
    }
}
