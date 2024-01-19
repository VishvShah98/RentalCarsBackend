using System.ComponentModel.DataAnnotations;

namespace RentalCarsBackend.Models
{
    public class RegisterModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // ConfirmPassword is not included as it's for frontend validation only

        public RegisterModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
