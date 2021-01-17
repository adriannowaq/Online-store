using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Extensions;
using OnlineStore.Models.Account.Admin;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AdminController : Controller
    {
        private readonly IProductRepository productRepository;

        public AdminController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult AddProduct() => View();

        [HttpGet]
        public async Task<IActionResult> EditProductAsync(int id)
        {
            var product = await productRepository.FindByIdForAdminAsync(id);
            if (product != null)
            {
                return View(new EditProductModel(product));
            }
            return RedirectToAction(nameof(HomeController.Index),
                nameof(HomeController).RemoveController());
        }

        #region ApiMethods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductModel productDetails)
        {
            if (ModelState.IsValid)
            {
                var productId = await productRepository.AddAsync(productDetails);

                return Ok(new
                {
                    RedirectUrl = Url.Action(nameof(ShopController.Details),
                        nameof(ShopController).RemoveController(), new { Id = productId })
                });
            }
            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductModel productDetails)
        {
            if (ModelState.IsValid)
            {
                await productRepository.EditAsync(productDetails);

                return Ok(new
                {
                    RedirectUrl = Url.Action(nameof(ShopController.Details), 
                        nameof(ShopController).RemoveController(), new { productDetails.Id })
                });
            }
            return BadRequest();
        }
        #endregion
    }
}