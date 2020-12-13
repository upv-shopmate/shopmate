using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMate.Persistence.Relational
{
    internal abstract class RelationalRepository<T> : IAsyncRepository<T>
        where T : class
    {
        protected DbSet<T> Set { get; }

        public RelationalRepository(DbSet<T> set)
        {
            Set = set;
        }

        public abstract IQueryable<T> GetAll();

        public void Add(T entity) => Set.Add(entity);
        public Task AddAsync(T entity) => Set.AddAsync(entity).AsTask();

        public void AddMany(IEnumerable<T> entities) => Set.AddRange(entities);
        public Task AddManyAsync(IEnumerable<T> entities) => Set.AddRangeAsync(entities);

        public void Remove(T entity) => Set.Remove(entity);

        public void RemoveMany(IEnumerable<T> entities) => Set.RemoveRange(entities);

        public void Update(T entity) => Set.Update(entity);

        public void UpdateMany(IEnumerable<T> entities) => Set.UpdateRange(entities);
    }
}
