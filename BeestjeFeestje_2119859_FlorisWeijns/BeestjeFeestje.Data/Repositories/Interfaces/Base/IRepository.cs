using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeestjeFeestje.Data.Repositories.Interfaces.Base
{
    public interface IRepository<T, in TKey> where T : class
    {
        ValueTask<T> Get(TKey id);
        Task<IEnumerable<T>> GetAll();
        ValueTask<T> Add(T entity);
        Task Update(T entity);
        Task Update(IEnumerable<T> entities);
        Task Delete(T id);
    }
}
