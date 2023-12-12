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

namespace Persistence.Repositories.MenuRepositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationContext _context;

        public MenuRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<Menu, bool>> expression)
        {
            return await _context.Menus.AnyAsync(expression);
        }

        public async Task<Menu> CreateMenu(Menu menu)
        {
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();

            return menu;
        }

        public async Task<bool> DeleteMenu(Menu menu)
        {
             _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Menu> EditMenuAsync(Menu menu)
        {
             _context.Menus.Update(menu);
            await _context.SaveChangesAsync();

            return menu;
        }

        public async Task<IList<Menu>> GetAllDraftMenusByCompanyNameAsync(string companyName)
        {
            var menus = await _context.Menus
                .Where(b => b.CompanyName == companyName && b.Status == Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft)
                .AsNoTracking()
                .ToListAsync();

            return menus;
        }

        public async Task<IList<Menu>> GetAllMenusByCompanyNameAsync(string companyName)
        {
            var menus = await _context.Menus
                .Include(m => m.MenuGroups)
                .ThenInclude(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .Where(b => b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return menus;
                
        }

        public async Task<Menu> GetAsync(Expression<Func<Menu, bool>> expression)
        {
            var menu =  await _context.Menus
                .Include(m => m.MenuGroups)
                .ThenInclude(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return menu;
        }

        public async Task<Menu> GetMenuByIdAsync(Guid menuId)
        {
            var menu = await _context.Menus
                .Include(mg => mg.MenuGroups)
                .ThenInclude(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .Where(m => m.Id == menuId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return menu;
        }

        public bool IsMenuCodeInUse(string menuCode)
        {
            return _context.Menus.Any(m => m.MenuCode == menuCode);
        }

        public async Task<bool> MenuExistsByNameAndCompanyName(string menuName, string companyName)
        {
            return await _context.Menus.AnyAsync(r => r.MenuName == menuName && r.CompanyName == companyName);
        }
    }
}
