using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using Microsoft.Extensions.DependencyInjection;

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

            var jungleType = context.Types.FirstOrDefault(a => a.Name == "Jungle");
            var snowType = context.Types.FirstOrDefault(a => a.Name == "Sneeuw");
            var farmType = context.Types.FirstOrDefault(a => a.Name == "Boerderij");
            var desertType = context.Types.FirstOrDefault(a => a.Name == "Woestijn");
            var vipType = context.Types.FirstOrDefault(a => a.Name == "VIP");

            MakeAnimals(context, superFarmId, jungleType, farmType, snowType, desertType, vipType);
        }

        private static void MakeAnimals(BeestjeFeestjeDBContext context, string superFarmId, AType jungleType, AType farmType, AType snowType, AType desertType, AType vipType)
        {
            List<Animal> animals = new List<Animal>()
            {
                new Animal("Tarzan", "Aap", 8.0, "Een aap uit de jungle", jungleType, superFarmId, "https://png.pngtree.com/png-clipart/20201208/original/pngtree-vector-cartoon-cute-playing-monkey-material-clipart-monkey-clipart-png-image_5532785.jpg"),
                new Animal("Kolonel", "Olifant", 7.4, "Een olifant uit de jungle", jungleType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Marty", "Zebra", 6.5, "Een zebra uit de jungle", jungleType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Alex", "Leeuw", 8.9, "Een leeuw uit de jungle", jungleType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Doug", "Hond", 4.0, "Een hond van de boerderij", farmType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Eddie", "Ezel", 4.5, "Een ezel van de boerderij", farmType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Otis", "Koe", 4.8, "Een koe van de boerderij", farmType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Duke", "Eend", 3.5, "Een eend van de boerderij", farmType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Duckling", "Kuiken", 3.7, "Een kuiken van de boerderij", farmType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Skipper", "Penguin", 4.6, "Een penguin uit de sneeuw", snowType, superFarmId, "https://cdn.pixabay.com/photo/2014/04/02/11/13/penguin-305574_640.png"),
                new Animal("Desmond", "Ijsbeer", 7.8, "Een ijsbeer uit de sneeuw", snowType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Pieter", "Zeehond", 6.7, "Een zeehond uit de sneeuw", snowType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Juistja", "Kameel", 6.3, "Een kameel uit de woestijn", desertType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Ka", "Slang", 5.4, "Een slang uit de woestijn", desertType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Monty", "T-Rex", 7.4, "Een t-rex uit het verleden", vipType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Charlie", "Eenhoorn", 7.4, "Een eenhoorn uit een sprookje", vipType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
            };

            foreach (var animal in animals)
            {
                if (!context.Animals.Any(a => a.Name == animal.Name))
                {
                    context.Animals.Add(animal);
                    context.SaveChanges();
                }

            }
            return;
        }
    }
    
}

