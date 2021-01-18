using OnlineStore.Data;

namespace OnlineStore.Models.Account
{
    public class AccountViewModel
    {
        public Address UserDetails { get; set; }
        public Address ShippingDetails { get; set; }
        public Order OrderDetails { get; set; }
    }
}
