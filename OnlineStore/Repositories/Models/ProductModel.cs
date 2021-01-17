using OnlineStore.Data;

namespace OnlineStore.Repositories.Models
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public int CountAll { get; set; }
        public double? AverageRate { get; set; }
        public string CategoryName { get; set; }
    }
}
