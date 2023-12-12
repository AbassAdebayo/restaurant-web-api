using Application.Interfaces.Modules.MenuSettings.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.MenuSettingsRepositories
{
    public class ModifierGroupRepository : IModifierGroupRepository
    {
        private readonly ApplicationContext _context;

        public ModifierGroupRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<ModifierGroup, bool>> expression)
        {
            return await _context.ModifierGroups.AnyAsync(expression);
        }

        public async Task<ModifierGroup> CreateModifierGroup(ModifierGroup modifierGroup)
        {
            await _context.ModifierGroups.AddAsync(modifierGroup);
            await _context.SaveChangesAsync();

            return modifierGroup;
        }

        public async Task<bool> DeleteModifierGroup(ModifierGroup modifierGroup)
        {
            _context.ModifierGroups.Remove(modifierGroup);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ModifierGroup> EditModifierGroupAsync(ModifierGroup modifierGroup)
        {
            _context.ModifierGroups.Update(modifierGroup);
            await _context.SaveChangesAsync();

            return modifierGroup;
        }

        public async Task<IList<ModifierGroup>> GetAllDraftModifierGroupsByCompanyNameAsync(string companyName)
        {
            var modifierGroups = await _context.ModifierGroups
                .Where(mg => mg.CompanyName == companyName && mg.Status == Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft)
                .AsNoTracking()
                .ToListAsync();

            return modifierGroups;
        }

        public async Task<IList<ModifierGroup>> GetAllModifierGroupsByCompanyNameAsync(string companyName)
        {
            var modifierGroups = await _context.ModifierGroups
                .Include(mg => mg.ModifierItems)
                .Where(mg => mg.CompanyName == companyName)
                .AsNoTracking()
                .ToListAsync();

            return modifierGroups;
        }

        public async Task<ModifierGroup> GetAsync(Expression<Func<ModifierGroup, bool>> expression)
        {
            var modifierGroup = await _context.ModifierGroups
                .Include(mg => mg.ModifierItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return modifierGroup;
        }

        public async Task<ModifierGroup> GetModifierGroupByIdAsync(Guid modifierGroupId)
        {
            var modifierGroup = await _context.ModifierGroups
                .Include(mg => mg.ModifierItems)
                .Where(mg => mg.Id == modifierGroupId)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            return modifierGroup;
        }

        public async Task<bool> ModifierGroupExistsByNameAndCompanyName(string modifierGroupName, string companyName)
        {
            return await _context.ModifierGroups.AnyAsync(r => r.ModifierGroupName == modifierGroupName && r.CompanyName == companyName);
        }
    }
}
