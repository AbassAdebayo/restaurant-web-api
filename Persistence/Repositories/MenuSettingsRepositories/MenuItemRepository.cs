using Application.Interfaces.Modules.MenuSettings.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.MenuSettingsRepositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly ApplicationContext _context;

        public MenuItemRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<bool> AnyAsync(Expression<Func<MenuItem, bool>> expression)
        {
            return await _context.MenuItems.AnyAsync(expression);
        }

        public async Task<MenuItem> CreateMenuItem(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();

            return menuItem;
        }

        public async Task<bool> DeleteMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MenuItem> EditMenuItemAsync(MenuItem menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();

            return menuItem;
        }

        public async Task<IList<MenuItem>> GetAllDraftMenuItemsByCompanyNameAsync(string companyName)
        {
            var menuItems = await _context.MenuItems
                .Where(m => m.CompanyName == companyName && m.Status == Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft)
                .AsNoTracking()
                .ToListAsync();

            return menuItems;
        }

        public async Task<IList<MenuItem>> GetAllMenuItemsByCompanyNameAsync(string companyName)
        {
            var menuItems = await _context.MenuItems
                .Include(m => m.RangePrices)
                .Include(m => m.TimeSpecificPrice)
                .Where(m => m.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return menuItems;
        }

        public async Task<MenuItem> GetAsync(Expression<Func<MenuItem, bool>> expression)
        {
            var menuItem = await _context.MenuItems
                .Include(m => m.RangePrices)
                .Include(m => m.TimeSpecificPrice)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return menuItem;
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(Guid menuItemId)
        {
            var menuItem = await _context.MenuItems
                .Include(m => m.RangePrices)
                .Include(m => m.TimeSpecificPrice)
                .Where(m => m.Id == menuItemId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return menuItem;
        }

        public bool IsMenuItemCodeInUse(string menuItemCode)
        {
            return _context.MenuItems.Any(m => m.MenuItemCode == menuItemCode);
        }

        public async Task<bool> MenuItemExistsByNameAndCompanyName(string menuItemName, string companyName)
        {
            return await _context.MenuItems.AnyAsync(r => r.MenuItemName == menuItemName && r.CompanyName == companyName);
        }

        public void Reload(MenuItem menuItem)
        {
            _context.Entry(menuItem).Reload();
        }
    }
}
