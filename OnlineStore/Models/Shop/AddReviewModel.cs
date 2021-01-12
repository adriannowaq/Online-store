using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Shop
{
    public class AddReviewModel
    {       
        [Display(Name = "Opinia")]
        [Range(1, 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Oceń produkt!")]
        public int Rate { get; set; }

        [Range(1, int.MaxValue)]
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Nazwa")]
        [StringLength(500, ErrorMessage = "Maksymalna długość to 500 znaków.")]
        public string Comment { get; set; }

        [Display(Name = "Autor")]
        [Required]
        [StringLength(25, ErrorMessage = "Maksymalna długość to 25 znaków.")]
        public string AuthorName { get; set; }
    }
}
