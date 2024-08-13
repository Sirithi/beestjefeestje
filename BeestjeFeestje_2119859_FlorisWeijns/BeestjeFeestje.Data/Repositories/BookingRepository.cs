using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje.Data.Repositories
{
    public class BookingRepository : Repository<Booking, string>, IBookingRepository
    {
        public BookingRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetAllWithRelations()
        {
            var bookings = await GetQuery().Include(b => b.Animals).Include(b => b.User).ToListAsync();
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetByUser(User user)
        {
            var bookings = await GetQuery().Where(b => b.User == user).ToListAsync();
            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetByUserWithRelations(User user)
        {
            var bookings = await GetQuery().Include(b => b.Animals).Include(b => b.User).Where(b => b.User == user).ToListAsync();
            return bookings;
        }
    }
}
