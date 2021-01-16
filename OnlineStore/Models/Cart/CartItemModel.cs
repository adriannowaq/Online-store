using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Cart
{
    public class CartItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, byte.MaxValue)]
        public byte Count { get; set; }
    }
}
