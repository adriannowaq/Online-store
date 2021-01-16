using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineStore.Data
{
    public class Cart
    {
        public int Id { get; set; }
        public bool Ordered { get; set; }
        public virtual List<CartItem> CartItems { get; set; } = new List<CartItem>();

        [NotMapped]
        public decimal SummaryCost { get; set; }

        public string Token { get; set; }

        [JsonIgnore]
        public int? UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
