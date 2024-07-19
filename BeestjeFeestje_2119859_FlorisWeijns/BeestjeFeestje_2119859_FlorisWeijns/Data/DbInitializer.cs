using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BeestjeFeestje_2119859_FlorisWeijns.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Controleer of de rol bestaat, zo niet dan maakt deze aan
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Controleer of de supergebruiker al bestaat
            var superUserEmail = "superuser@example.com";
            if (userManager.Users.All(u => u.Email != superUserEmail))
            {
                var superUser = new IdentityUser
                {
                    UserName = superUserEmail,
                    Email = superUserEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(superUser, "SuperSecretPassword123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superUser, "Admin");
                }
            }
        }
    }
}
