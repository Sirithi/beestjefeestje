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
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllByFarm(string id)
        {
            var result = await GetQuery().Where(u => u.FarmId == id).ToListAsync();
            return result;
        }
    }
}
