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
    }
}
