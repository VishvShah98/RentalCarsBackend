using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCarsBackend.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RentalCarsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        // GET: api/Bookings
        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var bookings = await _context.Bookings.ToListAsync();

            return Ok(bookings);
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var car = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == model.CarId);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);


                if (car == null || user == null)
                {
                    return NotFound(new { message = "Car or user not found" });
                }

                var booking = new Booking
                {
                    UserId = model.UserId,
                    CarId = model.CarId,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TotalPrice = model.TotalPrice,
                    BookingStatus = model.BookingStatus
                };

                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Booking created successfully" });
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Bookings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound(new { message = "Booking not found" });
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking deleted successfully" });
        }
    }
}
