using Microsoft.AspNetCore.Http;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Account.Admin
{
    public class AddProductModel
    {
        [Display(Name = "Nazwa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        public string Name { get; set; }

        [Display(Name = "Producent")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        public string Producer { get; set; }

        [Display(Name = "Cena")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        [RegularExpression(@"^\d+(\,\d{1,2})?$", ErrorMessage = "Niepoprawna kwota.")]
        public decimal Price { get; set; }

        [Display(Name = "Opis")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        public string Description { get; set; }

        [Display(Name = "Liczba")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Niepoprawna liczba.")]
        public int Count { get; set; }

        [Display(Name = "Kategoria")]
        [Range(1, int.MaxValue, ErrorMessage = "Pole wymagane.")]
        public int ProductCategory { get; set; }

        [Display(Name = "Zdjęcie")]
        [Required(ErrorMessage = "Pole wymagane.")]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maksymalny rozmiar zdjęcia to 3 mb.")]
        [AllowedContentTypes(new string[] { "image/gif", "image/jpg", "image/jpeg", "image/png" }, 
            ErrorMessage = "Dozwolone pliki .gif, .jpg, .png, .jpeg")]
        public IFormFile ImageFile { get; set; }
    }
}
