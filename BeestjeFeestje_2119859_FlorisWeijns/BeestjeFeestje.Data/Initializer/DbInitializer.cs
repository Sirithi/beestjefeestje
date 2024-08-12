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
            using var context = new BeestjeFeestjeDBContext(
            serviceProvider.GetRequiredService<DbContextOptions<BeestjeFeestjeDBContext>>());
            context.Database.Migrate();
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
                new Animal("Tarzan", "Aap", 8.0, "Een aap uit de jungle", jungleType, superFarmId, "https://static.vecteezy.com/system/resources/previews/003/513/751/non_2x/cute-monkey-hanging-tree-cartoon-illustration-free-vector.jpg"),
                new Animal("Kolonel", "Olifant", 7.4, "Een olifant uit de jungle", jungleType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRxh6FRUA2OHUJQ7InKst3dpJiDq2KrkbWwjQ&s"),
                new Animal("Marty", "Zebra", 6.5, "Een zebra uit de jungle", jungleType, superFarmId, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/639a0fdd-c38f-44ae-8c66-66a1bb7d3c60/du5uu6-c8d83d22-6b35-44bc-b8a2-a8009b118888.jpg/v1/fill/w_463,h_684,q_75,strp/marty__madagascar_by_supernaturalsarah_du5uu6-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9Njg0IiwicGF0aCI6IlwvZlwvNjM5YTBmZGQtYzM4Zi00NGFlLThjNjYtNjZhMWJiN2QzYzYwXC9kdTV1dTYtYzhkODNkMjItNmIzNS00NGJjLWI4YTItYTgwMDliMTE4ODg4LmpwZyIsIndpZHRoIjoiPD00NjMifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6aW1hZ2Uub3BlcmF0aW9ucyJdfQ.TbwxQnZoys3YG5WyEC50JNGPzBDXeXAmIaTzBm-KQEU"),
                new Animal("Alex", "Leeuw", 8.9, "Een leeuw uit de jungle", jungleType, superFarmId, "https://www.starcutouts.com/cdn/shop/files/967_SC_front.jpg?v=1683038217&width=1946"),
                new Animal("Doug", "Hond", 4.0, "Een hond van de boerderij", farmType, superFarmId, "https://i.ebayimg.com/images/g/yKYAAMXQrNtR0Ogq/s-l1200.jpg"),
                new Animal("Eddie", "Ezel", 4.5, "Een ezel van de boerderij", farmType, superFarmId, "https://modernfarmer.com/wp-content/uploads/2014/12/shrekfeature.jpg"),
                new Animal("Otis", "Koe", 4.8, "Een koe van de boerderij", farmType, superFarmId, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/474d848f-1446-4957-b1cf-381cb3b5f6b4/dggyegs-77716632-dd00-46ae-b2a5-cb1dc74b28bc.png/v1/fill/w_500,h_500/otis__barnyard_series__render_by_ahmad2345light_dggyegs-fullview.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9NTAwIiwicGF0aCI6IlwvZlwvNDc0ZDg0OGYtMTQ0Ni00OTU3LWIxY2YtMzgxY2IzYjVmNmI0XC9kZ2d5ZWdzLTc3NzE2NjMyLWRkMDAtNDZhZS1iMmE1LWNiMWRjNzRiMjhiYy5wbmciLCJ3aWR0aCI6Ijw9NTAwIn1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmltYWdlLm9wZXJhdGlvbnMiXX0.jXi5RD11OKpfc7NWk6MWibqCROUwwlGPMeUIAoMF1QQ"),
                new Animal("Duke", "Eend", 3.5, "Een eend van de boerderij", farmType, superFarmId, "https://t4.ftcdn.net/jpg/01/05/60/65/360_F_105606579_o1lRcMq5wgoTPcMYzJmXwI7GvsI0PsU4.jpg"),
                new Animal("Duckling", "Kuiken", 3.7, "Een kuiken van de boerderij", farmType, superFarmId, "https://zitebooks.com/wp-content/uploads/2015/11/Ugly-Duckling.jpg"),
                new Animal("Skipper", "Penguin", 4.6, "Een penguin uit de sneeuw", snowType, superFarmId, "https://www.giantbomb.com/a/uploads/scale_small/16/164658/2284324-skipper_characterbig.jpg"),
                new Animal("Desmond", "Ijsbeer", 7.8, "Een ijsbeer uit de sneeuw", snowType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNmSj_FJAqNYx0lJuX_NfQ2P9SDgx69Be0JQ&s"),
                new Animal("Seal", "Zeehond", 6.7, "Een zeehond uit de sneeuw", snowType, superFarmId, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQyUXDFubxhW4JhgOAjQEMJsSI7K7Wq4ZXq3w&s"),
                new Animal("Juistja", "Kameel", 6.3, "Een kameel uit de woestijn", desertType, superFarmId, "https://discourse.disneyheroesgame.com/uploads/default/original/3X/2/b/2b70fe7cb5b360dbeda5315f3759ca51be0d2d8b.png"),
                new Animal("Ka", "Slang", 5.4, "Een slang uit de woestijn", desertType, superFarmId, "https://dyn1.heritagestatic.com/lf?set=path%5B1%2F2%2F5%2F9%2F9%2F12599540%5D&call=url%5Bfile%3Aproduct.chain%5D"),
                new Animal("Monty", "T-Rex", 7.4, "Een t-rex uit het verleden", vipType, superFarmId, "https://t4.ftcdn.net/jpg/05/00/14/67/360_F_500146786_nRf0PG3EAe9kQADrZBNh8WTlGrNdjdUI.jpg"),
                new Animal("Charlie", "Eenhoorn", 7.4, "Een eenhoorn uit een sprookje", vipType, superFarmId, "https://media.tenor.com/LgHvfvEM_FMAAAAe/charlie-the-unicorn-they-took-my-kidney.png"),
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

