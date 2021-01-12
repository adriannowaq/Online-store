using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Models.Shop;
using OnlineStore.Repositories;
using System.Threading.Tasks;
using X.PagedList;
using System.Security.Claims;

namespace OnlineStore.Controllers
{
    public class ShopController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IReviewRepository reviewRepository;

        public ShopController(IProductRepository productRepository, IReviewRepository reviewRepository)
        {
            this.productRepository = productRepository;
            this.reviewRepository = reviewRepository;
        }

        [Route("[controller]/[action]/{id}")]
        [Route("[controller]/[action]/{id}/reviews", Name = "ShopDetailsReviews")]
        [HttpGet]
        public async Task<IActionResult> Details(int id, int pageNumber = 1, int pageSize = 10)
        {
            if (ModelState.IsValid)
            {
                var product = await productRepository.FindByIdAsync(id);
                if (product != null)
                {
                    var reviews = await reviewRepository.GetByProductIdAsync(id, pageNumber, pageSize);
                    var viewModel = new ShopDetailsViewModel
                    {
                        Product = product,
                        Reviews = new StaticPagedList<Review>(reviews, pageNumber, pageSize, product.CountAll)
                    };

                    return View(viewModel);
                }
            }

            return RedirectToAction(nameof(Index));
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

        #region ApiMethods
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview([FromForm] AddReviewModel reviewDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await reviewRepository.AddAsync(new Review
                    {
                        Rate = reviewDetails.Rate,
                        Comment = reviewDetails.Comment,
                        AuthorName = reviewDetails.AuthorName,
                        ProductId = reviewDetails.ProductId,
                        UserId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value)
                    });
                    return RedirectToRoute("ShopDetailsReviews", new { id = reviewDetails.ProductId });
                }
                catch (DbUpdateException) {}
            }
            return RedirectToAction(nameof(Details), new { id = reviewDetails.ProductId });
        }

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
