using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories.Interfaces
{
    public interface IFarmRepository : IRepository<Farm, string>
    {
        Task<Farm?> FindByName(string name);
    }
}
