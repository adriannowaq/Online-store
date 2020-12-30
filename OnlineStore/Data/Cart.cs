using System.Collections.Generic;

namespace OnlineStore.Data
{
    public class Cart
    {
        public int Id { get; set; }
        public bool Ordered { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public User User { get; set; }
    }
}
