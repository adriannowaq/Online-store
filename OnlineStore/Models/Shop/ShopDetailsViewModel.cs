using OnlineStore.Data;
using OnlineStore.Repositories.Models;
using X.PagedList;

namespace OnlineStore.Models.Shop
{
    public class ShopDetailsViewModel
    {
        public ProductModel Details { get; set; }
        public StaticPagedList<Review> Reviews { get; set; }
        
    }
}
