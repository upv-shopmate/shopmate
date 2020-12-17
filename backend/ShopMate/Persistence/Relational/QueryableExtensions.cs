using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Persistence.Relational
{
    public static class QueryableExtensions
    {
        public static IEnumerable<T> GetPage<T>(this IQueryable<T> query, int page, int itemsPerPage)
        => query
              .OrderBy(row => row) // order by key
              .Skip(page * itemsPerPage)
              .Take(itemsPerPage);

        /// <remarks>
        /// Contrary to the rest of overloads, this function evaluates the query internally in the client and thus it 
        /// may result in degraded performance.
        /// </remarks>
        public static IEnumerable<T> GetPage<T>(this IQueryable<T> query, int page, int itemsPerPage, out bool hasNext)
        {
            var items = query
                            .OrderBy(row => row) // order by key
                            .Skip(page * itemsPerPage)
                            .Take(itemsPerPage + 1)
                            .ToList();

            var count = items.Count();
            hasNext = count == itemsPerPage + 1;

            return items.SkipLast(1);
        }
    }
}
