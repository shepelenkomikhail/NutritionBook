using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NutritionalRecipeBook.Infrastructure.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIfNoTracking<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
        where T : class
    {
        return condition 
            ? source.AsNoTracking().Where(predicate) 
            : source;
    }
}
