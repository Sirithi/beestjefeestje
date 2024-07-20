using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeFeestje_2119859_FlorisWeijns.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Silver"))
            {
                await roleManager.CreateAsync(new IdentityRole("Silver"));
            }

            if (!await roleManager.RoleExistsAsync("Gold"))
            {
                await roleManager.CreateAsync(new IdentityRole("Gold"));
            }

            if (!await roleManager.RoleExistsAsync("Platinum"))
            {
                await roleManager.CreateAsync(new IdentityRole("Platinum"));
            }

            var superUserEmail = "superuser@example.com";
            var superUser = await userManager.FindByEmailAsync(superUserEmail);

            if (superUser == null)
            {
                superUser = new User
                {
                    UserName = superUserEmail,
                    Email = superUserEmail,
                    EmailConfirmed = true,
                    FarmId = "1" // Voorbeeld FarmId
                };

                var result = await userManager.CreateAsync(superUser, "SuperSecretPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(superUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                }
            }
        }
    }
}
