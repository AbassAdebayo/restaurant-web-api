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
    public class MenuGroupRepository : IMenuGroupRepository
    {
        private readonly ApplicationContext _context;

        public MenuGroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<MenuGroup, bool>> expression)
        {
            return await _context.MenuGroups.AnyAsync(expression);
        }

        public async Task<MenuGroup> CreateMenuGroup(MenuGroup menuGroup)
        {
            await _context.MenuGroups.AddAsync(menuGroup);
            await _context.SaveChangesAsync();

            return menuGroup;
        }

        public async Task<bool> DeleteMenuGroup(MenuGroup menuGroup)
        {
            _context.MenuGroups.Remove(menuGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<MenuGroup> EditMenuGroupAsync(MenuGroup menuGroup)
        {
            _context.MenuGroups.Update(menuGroup);
            await _context.SaveChangesAsync();

            return menuGroup;
        }

        public async Task<IList<MenuGroup>> GetAllDraftMenuGroupsByCompanyNameAsync(string companyName)
        {
            var menuGroups =  await _context.MenuGroups
                .Where(b => b.CompanyName == companyName && b.Status == Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft)
                .AsNoTracking()
                .ToListAsync();

            return menuGroups;
        }

        public async Task<IList<MenuGroup>> GetAllMenuGroupsByCompanyNameAsync(string companyName)
        {
            var menuGroups =  await _context.MenuGroups
                .Include(m => m.Menu)
                .Include(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .Where(b => b.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return menuGroups;
        }

        public async Task<MenuGroup> GetAsync(Expression<Func<MenuGroup, bool>> expression)
        {
            var menuGroup = await _context.MenuGroups
                .Include(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return menuGroup;

        }

        public async Task<MenuGroup> GetMenuGroupByIdAsync(Guid menuGroupId)
        {
            var menuGroup = await _context.MenuGroups
                .Include(m => m.Menu)
                .Include(mi => mi.MenuItems)
                .ThenInclude(rg => rg.RangePrices)
                .Where(mg => mg.Id == menuGroupId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return menuGroup;
        }

        public bool IsMenuGroupCodeInUse(string menuGroupCode)
        {
            return _context.MenuGroups.Any(m => m.MenuGroupCode == menuGroupCode);
        }

        public async Task<bool> MenuGroupExistsByNameAndCompanyName(string menuGroupName, string companyName)
        {
            return await _context.MenuGroups.AnyAsync(r => r.MenuGroupName == menuGroupName && r.CompanyName == companyName);
        }
    }
}
