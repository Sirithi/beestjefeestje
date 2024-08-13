﻿using BeestjeFeestje.Data.Entities;
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
    }
}
