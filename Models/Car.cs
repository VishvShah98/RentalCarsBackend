using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarsBackend.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string CarName { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        [Required]
        public decimal RentalPricePerDay { get; set; }

        public Car()
        {
            // Initialize string properties with empty string
            CarName = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            ImageURL = string.Empty;
        }

        // Add other car-related properties here, e.g., model, year, mileage
    }
}