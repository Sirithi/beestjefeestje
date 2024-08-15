using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories.Interfaces
{
    public interface IBookingRepository : IRepository<Booking, string>
    {
        public Task<IEnumerable<Booking>> GetByUser(User user);
        public Task<IEnumerable<Booking>> GetByUserWithRelations(User user);
        public Task<IEnumerable<Booking>> GetAllWithRelations();
        public Task<IEnumerable<Booking>> GetAllByFarmWithRelations(IEnumerable<string> ids);
        public Task<Booking> GetWithRelations(string id);
    }
}
