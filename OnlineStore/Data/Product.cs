namespace OnlineStore.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public int Count { get; set; }
        public bool Available { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
