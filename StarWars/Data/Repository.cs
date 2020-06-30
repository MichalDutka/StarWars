using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StarWars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly StarWarsContext dbContext;


        public Repository(StarWarsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> Create(T entity)
        {
            T item = (await dbContext.Set<T>().AddAsync(entity)).Entity;

            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Read(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public async Task Update(int id, T entity)
        {           

            dbContext.Set<T>().Update(entity);

            await dbContext.SaveChangesAsync();
        }

        public async Task<T> Delete(int id)
        {
            T item = await dbContext.Set<T>().FindAsync(id);

            if (item != null)
            {
                dbContext.Set<T>().Remove(item);
            }

            dbContext.SaveChanges();

            return item;
        }


        public async Task<bool> Exists(int id)
        {
            return await dbContext.Set<T>().FindAsync(id) != null;
        }

        public void Dispose() { }
    }
}
