using Microsoft.AspNetCore.Http;
using OnlineStore.Data;
using OnlineStore.Infrastructure.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Account.Admin
{
    public class EditProductModel
    {
        public EditProductModel() {}

        public EditProductModel(Product details)
        {
            this.Id = details.Id;
            this.Name = details.Name;
            this.Producer = details.Producer;
            this.Price = details.Price;
            this.Description = details.Description;
            this.Count = details.Count;
            this.ProductCategory = details.ProductCategoryId;
        }

        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

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
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = "Maksymalny rozmiar zdjęcia to 3 mb.")]
        [AllowedContentTypes(new string[] { "image/gif", "image/jpg", "image/jpeg", "image/png" },
            ErrorMessage = "Dozwolone pliki .gif, .jpg, .png, .jpeg")]
        public IFormFile ImageFile { get; set; }
    }
}
