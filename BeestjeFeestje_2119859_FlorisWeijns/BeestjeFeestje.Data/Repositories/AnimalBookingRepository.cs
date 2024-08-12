using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces;
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
    }
}
