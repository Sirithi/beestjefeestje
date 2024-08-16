using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeestjeFeestje.Domain.Models;

namespace BeestjeFeestje.Domain.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<AnimalModel> Get(string id);
        Task<AnimalModel> Add(AnimalModel animal, string farmId);
        Task<IEnumerable<AnimalModel>> GetAll();
        Task<bool> Delete(string id);
        Task<AnimalModel> Update(AnimalModel animal);
        Task<IEnumerable<AnimalModel>> GetByNames(IEnumerable<string> names);
        Task<IEnumerable<AnimalModel>> GetByNamesWithRelations(IEnumerable<string> names);
        Task<AnimalModel> GetByName(string name);
    }
}
