using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Persistence
{
    public interface IRepository<T>
        where T : class
    {
        public IQueryable<T> GetAll();

        public void Add(T entity);

        public void AddMany(IEnumerable<T> entities);
        public void AddMany(params T[] entities) => AddMany((IEnumerable<T>)entities);

        public void Remove(T entity);

        public void RemoveMany(IEnumerable<T> entities);
        public void RemoveMany(params T[] entities) => RemoveMany((IEnumerable<T>)entities);

        public void Update(T entity);

        public void UpdateMany(IEnumerable<T> entities);
        public void UpdateMany(params T[] entities) => UpdateMany((IEnumerable<T>)entities);
    }
}
