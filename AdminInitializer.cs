using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RentalCarsBackend.Models;
using System;
using System.Threading.Tasks;

namespace RentalCarsBackend
{
    public static class AdminInitializer
    {
        public static async Task CreateAdminUser(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var adminRoleName = "Admin";
            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
            }

            var adminUserName = configuration["Admin:UserName"];
            var adminEmail = configuration["Admin:Email"];
            var adminPassword = configuration["Admin:Password"];

            if (adminUserName != null && adminPassword != null)
            {
                var adminUser = await userManager.FindByNameAsync(adminUserName);
                if (adminUser == null)
                {
                    adminUser = new User { UserName = adminUserName, Email = adminEmail };
                    var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);
                    if (!createUserResult.Succeeded)
                    {
                        // Handle errors
                        throw new Exception("Admin user creation failed");
                    }
                }

                if (!await userManager.IsInRoleAsync(adminUser, adminRoleName))
                {
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
            }
        }
    }


}
