using Microsoft.AspNetCore.Mvc;
using OnlineStore.Repositories;
using System.Threading.Tasks;

namespace OnlineStore.Components
{
    public class NavigationCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public NavigationCategoryViewComponent(IProductCategoryRepository productCategoryRepository)
        {
            this.productCategoryRepository = productCategoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await productCategoryRepository.GetProductCategoriesAsync());
        }
    }
}
