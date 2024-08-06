using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories.Interfaces
{
    public interface IAnimalRepository : IRepository<Animal, string>
    {
        public Task<IEnumerable<Animal>> GetAllWIthRelations();
        public Task<Animal> GetWithRelations(string id);
    }
}
