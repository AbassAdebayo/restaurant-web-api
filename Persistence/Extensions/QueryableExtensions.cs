using Domain.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Filters<T> filters)
        {
            if (filters?.IsValid() == true)
                query = filters.Get().Aggregate(query, (current, filter) => current.Where(filter.Expression));
            return query;
        }

        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex, int limit, string? sortColumn = null)
        {
            int totalCount;
            try
            {
                totalCount = await query.CountAsync();
            }
            catch (InvalidOperationException)
            {
                var list = query.ToList();
                query = list.AsQueryable();
                totalCount = list.Count;
            }

            var collection = query;
            if (sortColumn != null)
            {
                collection = query
                   .OrderBy(sortColumn, false);
            }

            collection = collection.Skip((pageIndex - 1) * limit)
                                   .Take(limit);

            ICollection<T> rows;

            try
            {
                rows = await collection.ToListAsync();
            }
            catch (InvalidOperationException)
            {
                rows = collection.ToList();
            }

            return new PaginatedList<T>(rows, totalCount);
        }

        public static async Task<PaginatedResult<T>> ToPaginatedResultListAsync<T>(this IQueryable<T> query, int pageIndex, int limit, string? sortColumn = null)
        {
            int totalCount;
            try
            {
                totalCount = await query.CountAsync();
            }
            catch (InvalidOperationException)
            {
                var list = query.ToList();
                query = list.AsQueryable();
                totalCount = list.Count;
            }

            var collection = query;
            if (sortColumn != null)
            {
                collection = query
                   .OrderBy(sortColumn, false);
            }

            collection = collection.Skip((pageIndex - 1) * limit)
                                   .Take(limit);

            ICollection<T> rows;

            try
            {
                rows = await collection.ToListAsync();
            }
            catch (InvalidOperationException)
            {
                rows = collection.ToList();
            }

            return new PaginatedResult<T>(true, rows.ToList(), null, totalCount, pageIndex, limit);
        }


        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T, TKey>(this IQueryable<T> query, int pageIndex, int limit, Expression<Func<T, TKey>> keySelector) where T : class => await query.ToPaginatedListAsync(pageIndex, limit, keySelector.Name);
        public static async Task<PaginatedResult<T>> ToPaginatedResultListAsync<T, TKey>(this IQueryable<T> query, int pageIndex, int limit, Expression<Func<T, TKey>> keySelector) where T : class => await query.ToPaginatedResultListAsync(pageIndex, limit, keySelector.Name);

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            var command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
