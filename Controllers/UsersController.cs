using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RentalCarsBackend.Models;
using System.Threading.Tasks;

namespace RentalCarsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
            => (_userManager, _signInManager) = (userManager, signInManager);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use");
                    return BadRequest(ModelState);
                }

                var user = new User { UserName = model.Email, Email = model.Email }; // Use Email as UserName
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { userId = user.Id, message = "User registered successfully" }); // Return User ID
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(userName: model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        return Ok(new { userId = user.Id, message = "User logged in successfully" }); // Return User ID
                    }

                    if (result.IsLockedOut)
                    {
                        return BadRequest(new { message = "Account is locked out" });
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }


    }
}
