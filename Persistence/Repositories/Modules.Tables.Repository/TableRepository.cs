using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Tables;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Wrapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Tables.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly ApplicationContext _context;

        public TableRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Table> SaveTableAs(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();

            return table;
        }

        public async Task<bool> DeleteTable(Table table)
        {
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Table> EditTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();

            return table;
        }

        //public async Task<PaginatedList<Table>> GetPaginatedTablesByCompanyNameAsync(string companyName, PaginationFilter filter)
        //{
        //    //int skip = (filter.PageNumber - 1) * filter.PageSize;
        //    return await _context.Tables
        //        .Include(tn => tn.TabNumbers)
        //        .ThenInclude(tb => tb.Tabs)
        //        .ThenInclude(o => o.Orders)
        //        .ThenInclude(or => or.OrderItems)
        //        .Where(b => b.CompanyName == companyName)
        //        .AsNoTracking()
        //        .ToPaginatedListAsync(filter.PageNumber, filter.PageSize);

            
        //}

        public async Task<IList<Table>> GetAllTablesByCompanyNameAsync(string companyName)
        {
            return await _context.Tables
                .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(b => b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Table> GetTableByIdAsync(Guid Id)
        {
            var table = await _context.Tables
                .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(m => m.Id == Id)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return table;
        }

        public async Task<Table> GetTableByTableIdAsync(string tableId)
        {
            var table = await _context.Tables
               .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(m => m.TableId == tableId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return table;
        }

        public async Task<IList<Table>> GetAllOpenedTablesByCompanyNameAsync(string companyName)
        {
            return await _context.Tables
                .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(b => b.Status == Domain.Domain.Modules.Users.Entities.Enums.TableStatus.Opened && b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Table>> GetAllClosedTablesByCompanyNameAsync(string companyName)
        {
            return await _context.Tables
                .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(b => b.Status == Domain.Domain.Modules.Users.Entities.Enums.TableStatus.Closed && b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Table>> GetTablesDataByCompanyNameAsync(string companyName)
        {
            return await _context.Tables
                .Where(b => b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IList<Table>> GetAllTablesByBranchAndCompanyNameAsync(string companyName, string branchName)
        {
            return await _context.Tables
                .Include(tn => tn.Guests)
                .Include(tb => tb.Tabs)
                .ThenInclude(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(b => b.BranchName == branchName && b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
