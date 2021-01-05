using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
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

#region ApiMethods
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductModel productDetails)
        {
            if (ModelState.IsValid)
            {
                await productRepository.AddAsync(productDetails);

                return Ok(new
                {
                    RedirectUrl = Url.Action(nameof(HomeController.Index), "Home")
                });
            }
            return BadRequest();
        }
#endregion
    }
}