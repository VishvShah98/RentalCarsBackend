using System.ComponentModel.DataAnnotations;

namespace RentalCarsBackend.Models
{
    public class CarCreateModel
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public string CarName { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        [Required]
        public decimal RentalPricePerDay { get; set; }

        public CarCreateModel()
        {
            // Initialize string properties with empty string
            CarName = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            ImageURL = string.Empty;
        }
    }
}
