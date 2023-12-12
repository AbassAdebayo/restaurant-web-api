using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.MenuSettings.Repositories
{
    public interface IModifierItemRepository
    {
        public Task<ModifierItem> CreateModifierItem(ModifierItem modifierItem);
        public Task<ModifierItem> GetModifierItemByIdAsync(Guid modifierItemId);
        Task<bool> DeleteModifierItem(ModifierItem modifierItem);
        public Task<ModifierItem> EditModifierItemAsync(ModifierItem modifierItem);
        public Task<IList<ModifierItem>> GetAllModifierItemsByCompanyNameAsync(string companyName);
        public Task<IList<ModifierItem>> GetAllDraftModifierItemsByCompanyNameAsync(string companyName);
        public Task<ModifierItem> GetAsync(Expression<Func<ModifierItem, bool>> expression);
        public Task<bool> ModifierItemExistsByNameAndCompanyName(string modifierItemName, string companyName);
    }
}
