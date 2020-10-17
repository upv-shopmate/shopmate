using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.Persistence.Relational
{
    internal class RelationalRepository<T> : IAsyncRepository<T>
        where T : class
    {
        readonly DbSet<T> set;

        public RelationalRepository(DbSet<T> set)
        {
            this.set = set;
        }

        public void Add(T entity) => set.Add(entity);
        public Task AddAsync(T entity) => set.AddAsync(entity).AsTask();

        public void AddMany(IEnumerable<T> entities) => set.AddRange(entities);
        public Task AddManyAsync(IEnumerable<T> entities) => set.AddRangeAsync(entities);

        public IEnumerable<T> GetAll() => set;

        public T? GetById(params object[] id) => set.Find(id);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        public Task<T?> GetByIdAsync(params object[] id) => set.FindAsync(id).AsTask();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        public void Remove(T entity) => set.Remove(entity);

        public void RemoveMany(IEnumerable<T> entities) => set.RemoveRange(entities);

        public void Update(T entity) => set.Update(entity);

        public void UpdateMany(IEnumerable<T> entities) => set.UpdateRange(entities);
    }
}
