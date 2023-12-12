using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
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
    public class ModifierItemRepository : IModifierItemRepository
    {
        private readonly ApplicationContext _context;

        public ModifierItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ModifierItem> CreateModifierItem(ModifierItem modifierItem)
        {
            await _context.ModifierItems.AddAsync(modifierItem);
            await _context.SaveChangesAsync();

            return modifierItem;
        }

        public async Task<bool> DeleteModifierItem(ModifierItem modifierItem)
        {
            _context.ModifierItems.Remove(modifierItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ModifierItem> EditModifierItemAsync(ModifierItem modifierItem)
        {
            _context.ModifierItems.Update(modifierItem);
            await _context.SaveChangesAsync();

            return modifierItem;
        }

        public async Task<IList<ModifierItem>> GetAllDraftModifierItemsByCompanyNameAsync(string companyName)
        {
            var modifierItems = await _context.ModifierItems
               .Where(m => m.CompanyName == companyName && m.Status == Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft)
               .AsNoTracking()
               .ToListAsync();

            return modifierItems;
        }

        public async Task<IList<ModifierItem>> GetAllModifierItemsByCompanyNameAsync(string companyName)
        {
            var modifierItems = await _context.ModifierItems
                .Where(m => m.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return modifierItems;
        }

        public async Task<ModifierItem> GetAsync(Expression<Func<ModifierItem, bool>> expression)
        {
            var modifierItem = await _context.ModifierItems
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return modifierItem;
        }

        public async Task<ModifierItem> GetModifierItemByIdAsync(Guid modifierItemId)
        {
            var modifierItem = await _context.ModifierItems
                .Where(m => m.Id == modifierItemId)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            return modifierItem;
        }

        public async Task<bool> ModifierItemExistsByNameAndCompanyName(string modifierItemName, string companyName)
        {
            return await _context.ModifierItems.AnyAsync(r => r.ModifierItemName == modifierItemName && r.CompanyName == companyName);
        }
    }
}
