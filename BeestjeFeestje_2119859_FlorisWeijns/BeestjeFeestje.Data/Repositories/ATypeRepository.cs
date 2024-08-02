using BeestjeFeestje.Data.Contexts;
using BeestjeFeestje.Data.Entities;
using BeestjeFeestje.Data.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories
{
    public class ATypeRepository : Repository<AType, string>, IATypeRepository
    {
        public ATypeRepository(BeestjeFeestjeDBContext context) : base(context)
        {
        }
    }
}
