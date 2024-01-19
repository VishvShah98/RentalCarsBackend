using System.ComponentModel.DataAnnotations;

namespace RentalCarsBackend.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public string BookingStatus { get; set; }
        public Booking()
        {
            // Initialize string properties with empty string
            BookingStatus = string.Empty;
            UserId = string.Empty;
        }

        // Add other booking-related properties here, e.g., additional options
    }
}
