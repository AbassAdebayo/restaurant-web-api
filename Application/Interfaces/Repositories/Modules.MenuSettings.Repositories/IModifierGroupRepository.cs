using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Repositories
{
    public interface IModifierGroupRepository
    {
        public Task<ModifierGroup> CreateModifierGroup(ModifierGroup modifierGroup);
        public Task<ModifierGroup> GetModifierGroupByIdAsync(Guid modifierGroupId);
        Task<bool> DeleteModifierGroup(ModifierGroup modifierGroup);
        public Task<ModifierGroup> EditModifierGroupAsync(ModifierGroup modifierGroup);
        public Task<IList<ModifierGroup>> GetAllModifierGroupsByCompanyNameAsync(string companyName);
        public Task<IList<ModifierGroup>> GetAllDraftModifierGroupsByCompanyNameAsync(string companyName);
        public Task<bool> AnyAsync(Expression<Func<ModifierGroup, bool>> expression);
        public Task<ModifierGroup> GetAsync(Expression<Func<ModifierGroup, bool>> expression);
        public Task<bool> ModifierGroupExistsByNameAndCompanyName(string modifierGroupName, string companyName);
    }
}
