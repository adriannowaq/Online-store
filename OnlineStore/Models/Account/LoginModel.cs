using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Account
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Pole wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
