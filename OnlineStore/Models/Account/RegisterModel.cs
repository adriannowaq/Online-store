using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Account
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane pole.")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy adres email")]
        [Remote("CheckEmailExists", "Account", ErrorMessage = "Podany email jest już zajęty.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane pole.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Wymagane pole.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Hasła nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }
}
