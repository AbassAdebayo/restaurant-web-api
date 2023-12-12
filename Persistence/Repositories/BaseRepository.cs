using Application.Interfaces;
using Domain.Contracts;
using Domain.Wrapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected ApplicationContext _context;
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<PaginatedList<T>> GetAsync(Expression<Func<T, bool>> expression, PaginationFilter filter)
        {
            return await _context.Set<T>().Where(expression).AsNoTracking().ToPaginatedListAsync(filter.PageSize, filter.PageNumber);
        }

        public async Task<PaginatedList<T>> GetAsync(PaginationFilter filter)
        {
            return await _context.Set<T>().AsNoTracking().ToPaginatedListAsync(filter.PageSize, filter.PageNumber);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().CountAsync(expression);
        }

        public async Task<int> GetDistinctCountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Distinct().CountAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task UpdateRangeAsync(List<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task DeleteRangeAsync(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
