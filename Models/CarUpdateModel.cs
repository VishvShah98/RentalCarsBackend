namespace RentalCarsBackend.Models
{
    public class CarUpdateModel
    {
        public string Category { get; set; }
        public string CarName { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public decimal? RentalPricePerDay { get; set; }

        public CarUpdateModel()
        {
            // Initialize string properties with empty string
            CarName = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            ImageURL = string.Empty;
        }
    }
}
