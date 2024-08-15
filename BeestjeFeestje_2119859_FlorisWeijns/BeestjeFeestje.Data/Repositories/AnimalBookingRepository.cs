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
    internal class AnimalBookingRepository : Repository<AnimalBooking, string>, IAnimalBookingRepository
    {
        public AnimalBookingRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AnimalBooking>> GetAllWithRelations()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnimalBooking>> GetByAnimal(Animal animal)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnimalBooking>> GetByAnimalWithRelations(Animal animal)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AnimalBooking>> GetByBooking(Booking booking)
        {
            var animalBookings = await GetQuery().Where(ab => ab.BookingId == booking.Id).ToListAsync();
            return animalBookings;
        }

        public async Task<IEnumerable<AnimalBooking>> GetByBookingWithRelations(Booking booking)
        {
            throw new NotImplementedException();
        }

        public async Task<AnimalBooking> GetWithRelations(string id)
        {
            throw new NotImplementedException();
        }
    }
}
