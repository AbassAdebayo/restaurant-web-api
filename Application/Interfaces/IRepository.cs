using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<PaginatedList<T>> GetAsync(Expression<Func<T, bool>> expression, PaginationFilter filter);
        Task<PaginatedList<T>> GetAsync(PaginationFilter filter);
        Task<int> GetCountAsync(Expression<Func<T, bool>> expression);
        Task<int> GetDistinctCountAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entities);
        void SaveChanges();
    }
}
