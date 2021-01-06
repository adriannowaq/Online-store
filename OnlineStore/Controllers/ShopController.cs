using Microsoft.AspNetCore.Mvc;
using OnlineStore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository productRepository;

        public ShopController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }

        #region ApiMethods
        [HttpGet]
        public async Task<IActionResult> SearchProducts([FromQuery] string name)
        {
            var products = await productRepository.SearchProductsByLettersAsync(name, 6);

            return Ok(new { products });
        }
        #endregion
    }
}
