using BeestjeFeestje.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BeestjeFeestje.Data.Contexts
{
    public class BeestjeFeestjeDBContext(DbContextOptions<BeestjeFeestjeDBContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AType> Types { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<AnimalBooking> AnimalBookings { get; set; }
    }
}
