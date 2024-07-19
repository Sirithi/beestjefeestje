using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje_2119859_FlorisWeijns.Data
{
    public class BeestjeFeestjeDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public BeestjeFeestjeDBContext(DbContextOptions<BeestjeFeestjeDBContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
    }
}
