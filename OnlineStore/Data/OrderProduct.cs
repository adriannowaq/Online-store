namespace OnlineStore.Data
{
    public class OrderProduct
    {
        public OrderProduct() {}

        public OrderProduct(CartItem cartItem)
        {
            this.Name = cartItem.Product.Name;
            this.Producer = cartItem.Product.Producer;
            this.Price = cartItem.Cost;
            this.CloudStorageImageName = cartItem.Product.CloudStorageImageName;
            this.CloudStorageImageUrl = cartItem.Product.CloudStorageImageUrl;
            this.Count = cartItem.Count;
            this.ProductId = cartItem.Product.Id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public decimal Price { get; set; }
        public string CloudStorageImageName { get; set; }
        public string CloudStorageImageUrl { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
