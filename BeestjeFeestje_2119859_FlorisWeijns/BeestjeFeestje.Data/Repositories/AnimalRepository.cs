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

        public async Task<IEnumerable<Animal>> GetAllByIdWithRelations(IEnumerable<string> ids)
        {
            var result = await GetQuery().Include(a => a.AnimalType).Where(a => ids.Contains(a.Id)).ToListAsync();
            return result;
        }

        public async Task<Animal> GetByName(string name)
        {
            var result = await GetQuery().Where(a => a.Name == name).FirstOrDefaultAsync();
            return result ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<Animal>> GetByNames(IEnumerable<string> names)
        {
            return await GetQuery().Where(a => names.Contains(a.Name)).ToListAsync();
        }

        public async Task<Animal> GetWithRelations(string id)
        {
            var animal = await GetQuery().Include(a => a.AnimalType).Where(a => a.Id == id).FirstOrDefaultAsync();
            return animal ?? throw new KeyNotFoundException();
        }

        public async Task<IEnumerable<Animal>> GetByNamesWithRelations(IEnumerable<string> names)
        {
            var result = await GetQuery().Include(a => a.AnimalType).Where(a => names.Contains(a.Name)).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Animal>> GetByFarmWithRelations(string farmId)
        {
            var result = await GetQuery().Include(a => a.AnimalType).Where(a => a.FarmId == farmId).ToListAsync();
            return result;
        }
    }
}
