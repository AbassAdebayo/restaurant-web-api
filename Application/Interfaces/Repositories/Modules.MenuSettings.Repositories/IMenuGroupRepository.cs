using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Repositories
{
    public interface IMenuGroupRepository
    {
        public Task<MenuGroup> CreateMenuGroup(MenuGroup menuGroup);
        public Task<MenuGroup> GetMenuGroupByIdAsync(Guid menuGroupId);
        Task<bool> DeleteMenuGroup(MenuGroup menuGroup);
        public Task<MenuGroup> EditMenuGroupAsync(MenuGroup menuGroup);
        public Task<IList<MenuGroup>> GetAllMenuGroupsByCompanyNameAsync(string companyName);
        public Task<IList<MenuGroup>> GetAllDraftMenuGroupsByCompanyNameAsync(string companyName);
        public Task<bool> AnyAsync(Expression<Func<MenuGroup, bool>> expression);
        public Task<MenuGroup> GetAsync(Expression<Func<MenuGroup, bool>> expression);
        public bool IsMenuGroupCodeInUse(string menuGroupCode);
        public Task<bool> MenuGroupExistsByNameAndCompanyName(string menuGroupName, string companyName);
    }
}
