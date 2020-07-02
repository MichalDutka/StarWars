using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWars
{
    public interface IRepository<T> where T : class
    {
        Task<T> Create(T entity);

        Task<IEnumerable<T>> ReadAll();

        Task<T> Read(int id);

        Task<T> Update(T entity);

        Task<T> Delete(int id);
    }
}
