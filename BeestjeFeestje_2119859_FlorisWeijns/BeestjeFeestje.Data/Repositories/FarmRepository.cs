﻿using BeestjeFeestje.Data.Contexts;
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
    public class FarmRepository : Repository<Farm, string>, IFarmRepository
    {
        public FarmRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public Task<Farm?> FindByName(string name)
        {
            return GetQuery().Where(f => f.FarmName == name).FirstOrDefaultAsync();
        }
    }
}
