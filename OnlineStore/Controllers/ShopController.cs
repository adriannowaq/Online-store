using Microsoft.AspNetCore.Mvc;
using OnlineStore.Repositories;
using OnlineStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineStore.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository productRepository;
        public ShopController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? category,
                                               [FromQuery] int page = 1,
                                               [FromQuery] int order = 1)
        {
            ViewBag.CategoryId = category ?? 0;

            var products = await productRepository.GetProductsPerPageAsync(category, page, 9, order);
            var productCount = await productRepository.CountProductsAsync(category);
            var productList = new StaticPagedList<Product>(products, page, 9, productCount);

            return View(productList);
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

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> AddItemToCart([FromBody] int productId)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }

        #endregion
    }
}
