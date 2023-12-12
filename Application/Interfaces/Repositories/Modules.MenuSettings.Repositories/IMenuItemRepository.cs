using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Repositories
{
    public interface IMenuItemRepository
    {
        public Task<MenuItem> CreateMenuItem(MenuItem menuItem);
        public Task<MenuItem> GetMenuItemByIdAsync(Guid menuItemId);
        Task<bool> DeleteMenuItem(MenuItem menuItem);
        public Task<MenuItem> EditMenuItemAsync(MenuItem menuItem);
        public Task<IList<MenuItem>> GetAllMenuItemsByCompanyNameAsync(string companyName);
        public Task<IList<MenuItem>> GetAllDraftMenuItemsByCompanyNameAsync(string companyName);
        public Task<bool> AnyAsync(Expression<Func<MenuItem, bool>> expression);
        public Task<MenuItem> GetAsync(Expression<Func<MenuItem, bool>> expression);
        public bool IsMenuItemCodeInUse(string menuItemCode);
        public Task<bool> MenuItemExistsByNameAndCompanyName(string menuItemName, string companyName);
        public void Reload(MenuItem menuItem);
    }
}
