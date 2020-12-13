using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PopulateDb
{
    public static class StringExtensions
    {
        public static string LimitLength(this string source, int maxLength)
        {
            if (source.Length <= maxLength)
            {
                return source;
            }

            return source.Substring(0, maxLength);
        }
    }

    public static class DbExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MatchingOrNew<T>(this DbSet<T> set, ICollection<T> cache, T reference)
             where T : class, IEquatable<T>
        {
            var cacheMatch = cache.FirstOrDefault(e => e.Equals(reference));
            if (!(cacheMatch is null))
            {
                return cacheMatch;
            }

            return set.MatchingOrNew(reference);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MatchingOrNew<T>(this DbSet<T> set, T reference)
            where T : class, IEquatable<T>
        {
            var dbMatch = set.FirstOrDefault(e => e.Equals(reference));
            if (!(dbMatch is null))
            {
                return dbMatch;
            }

            return reference;
        }
    }
}
