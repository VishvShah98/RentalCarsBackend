using System.ComponentModel.DataAnnotations;

namespace RentalCarsBackend.Models
{
    public class BookingCreateModel
    {
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

        public BookingCreateModel() {

            BookingStatus = string.Empty; 
            UserId = string.Empty;
        
        }

    }
}
