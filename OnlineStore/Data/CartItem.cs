namespace OnlineStore.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
