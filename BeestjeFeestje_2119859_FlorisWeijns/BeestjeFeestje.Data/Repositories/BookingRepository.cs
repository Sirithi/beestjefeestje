using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories
{
    public class BookingRepository : Repository<Booking, string>, IBookingRepository
    {
        public BookingRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Booking>> GetByUser(User user)
        {
            var bookings = await GetQuery().Where(b => b.User == user).ToListAsync();
            return bookings;
        }
    }
}
