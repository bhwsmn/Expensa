using System;
using System.Linq;
using System.Linq.Expressions;

namespace Services.Helpers
{
    public static class EfLinqExtensions
    {
        public static IQueryable<T> ConditionalWhere<T>(
            this IQueryable<T> source,
            Func<bool> condition,
            Expression<Func<T, bool>> predicate
        )
        {
            return condition() ? source.Where(predicate) : source;
        }
    }
}