using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories.Interfaces
{
    public interface IAnimalBookingRepository : IRepository<AnimalBooking, string>
    {
        public Task<IEnumerable<AnimalBooking>> GetByBooking(Booking booking);
        public Task<IEnumerable<AnimalBooking>> GetByAnimal(Animal animal);
        public Task<IEnumerable<AnimalBooking>> GetByAnimalWithRelations(Animal animal);
        public Task<IEnumerable<AnimalBooking>> GetByBookingWithRelations(Booking booking);
        public Task<IEnumerable<AnimalBooking>> GetAllWithRelations();
        public Task<AnimalBooking> GetWithRelations(string id);
    }
}
