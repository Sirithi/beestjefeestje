using BeestjeFeestje.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Domain.Services.Interfaces
{
    public interface IAnimalTypeService
    {
        public Task<ATypeModel> GetById(string id);

        public Task<IEnumerable<ATypeModel>> GetAll();
    }
}
