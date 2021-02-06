using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Models.Home;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IReviewRepository reviewRepository;
        public HomeController(IProductRepository productRepostiory, IReviewRepository reviewRepository)
        {
            this.productRepository = productRepostiory;
            this.reviewRepository = reviewRepository;
        }
        public async Task<IActionResult> Index()
        {
            var viewData = new HomeViewModel
            {
                Products = await productRepository.GetMostPopularProductsAsync(),
                Reviews = await reviewRepository.GetLatestReviewsAsync(9)
            };
            return View(viewData);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
