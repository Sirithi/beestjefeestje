using Microsoft.AspNetCore.Identity;
using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje_2119859_FlorisWeijns.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            using var context = new BeestjeFeestjeDBContext(
            serviceProvider.GetRequiredService<DbContextOptions<BeestjeFeestjeDBContext>>());

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

            if (!await roleManager.RoleExistsAsync("Owner"))
            {
                await roleManager.CreateAsync(new IdentityRole("Owner"));
            }

            string superFarmId = "";

            try
            {
                string farmName = "SuperFarm";
                if (!await context.Farms.AnyAsync(f => f.FarmName == farmName))
                {
                    Farm farm = new Farm(farmName);
                    context.Farms.Add(farm);
                    await context.SaveChangesAsync();
                }

                Farm? dbFarm = context.Farms.FirstOrDefault(f => f.FarmName == farmName);
                if (dbFarm == null)
                {
                    throw new Exception("Farm not found");
                }

                superFarmId = dbFarm.Id;
            }
            catch (Exception e)
            {
                superFarmId = "1";
                Console.WriteLine(e.Message);
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
                    FarmId = superFarmId
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
            List<AType> aTypes = new List<AType>
            {
                 new AType("Jungle"),
                 new AType("Sneeuw"),
                 new AType("Boerderij"),
                 new AType("Woestijn"),
                 new AType("VIP")
            };

            aTypes.ForEach(aType =>
            {
                if (!context.Types.Any(a => a.Name == aType.Name))
                {
                    context.Types.Add(aType);
                    context.SaveChanges();
                }
            });

            //AType jungleType = new AType("Jungle");
            //AType snowType = new AType("Sneeuw");
            //AType farmType = new AType("Boerderij");
            //AType desertType = new AType("Woestijn");
            //AType vipType = new AType("VIP");

        }
    }
}
