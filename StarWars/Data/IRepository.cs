using StarWars.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity);

        Task<IEnumerable<T>> ReadAll();

        Task<T> Read(int id);

        Task Update(int id, T entity);

        Task<T> Delete(int id);

        Task<bool> Exists(int id);
    }
}
