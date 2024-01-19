using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentalCarsBackend.Models;

namespace RentalCarsBackend
{
    public class ApplicationDbContext(IConfiguration Configuration) : IdentityDbContext<User>
    {

        public IConfiguration Config { get; set; } = Configuration;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure Booking.TotalPrice
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.TotalPrice)
                    .HasPrecision(18, 2); // Sets the precision to 18 and scale to 2
            });

            // Configure Car.RentalPricePerDay
            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.RentalPricePerDay)
                    .HasPrecision(18, 2); // Sets the precision to 18 and scale to 2
            });

            // Configure Payment.Amount
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2); // Sets the precision to 18 and scale to 2
            });

            // Configure other entities or relationships if necessary
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(
                Config.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,       // Maximum number of retry attempts
                        maxRetryDelay: TimeSpan.FromSeconds(30), // Delay between retries
                        errorNumbersToAdd: null); // SQL error numbers to consider as transient
                });
        }



        // Define DbSet properties for each of your models
        public DbSet<Category> Categories { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // Optionally, configure relationships and model-specific constraints here

    }
}
