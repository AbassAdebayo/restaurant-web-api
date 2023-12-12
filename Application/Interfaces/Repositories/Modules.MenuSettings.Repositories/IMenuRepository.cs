using Application.DTOs.MenuSettingDtos;
using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Repositories
{
    public interface IMenuRepository
    {
        public Task<Menu> CreateMenu(Menu menu);
        public Task<Menu> GetMenuByIdAsync(Guid menuId);
        Task<bool> DeleteMenu(Menu menu);
        public Task<Menu> EditMenuAsync(Menu menu);
        public Task<IList<Menu>> GetAllMenusByCompanyNameAsync(string companyName);
        public Task<IList<Menu>> GetAllDraftMenusByCompanyNameAsync(string companyName);
        public Task<bool> AnyAsync(Expression<Func<Menu, bool>> expression);
        public Task<Menu> GetAsync(Expression<Func<Menu, bool>> expression);
        public bool IsMenuCodeInUse(string menuCode);
        public Task<bool> MenuExistsByNameAndCompanyName(string menuName, string companyName);


    }
}
