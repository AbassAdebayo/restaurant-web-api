using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.Tables;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Tables.Repository
{
    public class TabRepository : ITabRepository
    {
        private readonly ApplicationContext _context;

        public TabRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Tab> AddTabAsync(Tab tab)
        {
            await _context.Tabs.AddAsync(tab);
            await _context.SaveChangesAsync();

            return tab;
        }

        public async Task<IList<Tab>> GetAllTabsByCompanyAsync(string companyName)
        {
            var tabs = await _context.Tabs
                .Include(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(t => t.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return tabs;
        }

        public async Task<Tab> GetTabByIdAsync(Guid tabId)
        {
            var tab = await _context.Tabs
                .Include(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(t => t.Id == tabId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return tab;
        }

        public async Task<Tab> GetTabByNameAsync(string tabName)
        {
            var tab = await _context.Tabs
                .Include(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(t => t.TabName == tabName)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return tab;
        }

        public async Task<IList<Tab>> GetTabsForTableAsync(Guid tableId)
        {
            var tabs = await _context.Tabs
                .Include(o => o.Orders)
                .ThenInclude(or => or.OrderItems)
                .Where(t => t.TableId == tableId)
                .AsNoTracking()
                .ToListAsync();

            return tabs;
        }

        //public async Task<bool> TabExistsByNameAndCompanyName(string tabName, string companyName)
        //{
        //    return await _context.Tabs.AnyAsync(t => t.TabName == tabName && t.CompanyName == companyName);
        //}
    }
}
