using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalCarsBackend.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        public int BookingId { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.01")]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentStatus { get; set; }

        public Payment()
        {
            // Initialize string properties with empty string
            PaymentStatus = string.Empty;
        }

        // Add other payment-related properties here, e.g., payment method, transaction ID
    }
}
