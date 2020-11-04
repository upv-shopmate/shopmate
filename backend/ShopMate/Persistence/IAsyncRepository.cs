using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.Persistence
{
    public interface IAsyncRepository<T> : IRepository<T>
        where T : class
    {
        public Task AddAsync(T entity);

        public Task AddManyAsync(IEnumerable<T> entities);
        public Task AddManyAsync(params T[] entities) => AddManyAsync((IEnumerable<T>)entities);
    }
}
