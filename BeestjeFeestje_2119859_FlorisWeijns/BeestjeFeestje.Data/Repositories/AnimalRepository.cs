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
    public class AnimalRepository : Repository<Animal, string>, IAnimalRepository
    {
        public AnimalRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Animal>> GetAllWIthRelations()
        {
            return await GetQuery().Include(a => a.AnimalType).ToListAsync();
        }

        public async Task<Animal> GetWithRelations(string id)
        {
            var animal = await GetQuery().Include(a => a.AnimalType).Where(a => a.Id == id).FirstOrDefaultAsync();
            if (animal == null)
            {
                throw new KeyNotFoundException();
            }
            return animal;
        }
    }
}
