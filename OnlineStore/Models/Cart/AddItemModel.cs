using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Cart
{
    public class AddItemModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
