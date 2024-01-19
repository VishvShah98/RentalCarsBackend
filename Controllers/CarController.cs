using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsBackend.Models;
using System.Threading.Tasks;

namespace RentalCarsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   // [Authorize(Roles = "Admin")] // Requires "Admin" role for access
    public class CarController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        //Get All Cars
        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _context.Cars.ToListAsync();

            if (cars == null || cars.Count == 0)
            {
                return NotFound(new { message = "No cars found" });
            }

            return Ok(cars);
        }

        // GET Available Cars
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableCars([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate >= endDate)
            {
                return BadRequest(new { message = "Invalid date range" });
            }

            var bookedCarIds = await _context.Bookings
                .Where(b => (b.StartDate <= startDate && b.EndDate >= startDate) ||
                            (b.StartDate <= endDate && b.EndDate >= endDate) ||
                            (b.StartDate >= startDate && b.EndDate <= endDate))
                .Select(b => b.CarId)
                .ToListAsync();

            var availableCars = await _context.Cars
                .Where(c => !bookedCarIds.Contains(c.CarId))
                .ToListAsync();

            return Ok(availableCars);
        }


        //Create Car
        [HttpPost("create")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new Car entity from the DTO
                var car = new Car
                {
                    Category = model.Category,
                    CarName = model.CarName,
                    Description = model.Description,
                    ImageURL = model.ImageURL,
                    RentalPricePerDay = model.RentalPricePerDay
                };

                // Add the car to the database
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Car created successfully" });
            }

            return BadRequest(ModelState);
        }

        //Update Model
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FindAsync(id);

                if (car == null)
                {
                    return NotFound(new { message = $"Car with ID {id} not found" });
                }

                // Update the car entity with the new data, but only if the corresponding field is provided
                if (model.Category != null)
                {
                    car.Category = model.Category;
                }

                if (model.CarName != null)
                {
                    car.CarName = model.CarName;
                }

                if (model.Description != null)
                {
                    car.Description = model.Description;
                }

                if (model.ImageURL != null)
                {
                    car.ImageURL = model.ImageURL;
                }

                if (model.RentalPricePerDay.HasValue)
                {
                    car.RentalPricePerDay = model.RentalPricePerDay.Value;
                }

                // Save the changes to the database
                await _context.SaveChangesAsync();

                return Ok(new { message = "Car updated successfully" });
            }

            return BadRequest(ModelState);
        }

        //Delete Car
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Car deleted successfully" });
        }


    }
}
