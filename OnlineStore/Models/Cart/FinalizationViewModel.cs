using OnlineStore.Data;

namespace OnlineStore.Models.Cart
{
    public class FinalizationViewModel
    {
        public Order Order { get; set; }
        public Data.Cart Cart { get; set; }
    }
}
