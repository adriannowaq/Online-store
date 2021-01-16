using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OnlineStore.Data
{
    public class CartItem
    {
        public int Id { get; set; }
        public byte Count { get; set; }

        [NotMapped]
        public decimal Cost { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [JsonIgnore]
        public int CartId { get; set; }

        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
