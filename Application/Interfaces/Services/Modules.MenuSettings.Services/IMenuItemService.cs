using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Services
{
    public interface IMenuItemService
    {
        public Task<BaseResponse<MenuItem>> AddMenuItemAsync(string userToken, Guid menuGroupId, CreateMenuItemRequestModel request);
        public Task<BaseResponse<MenuItemDto>> GetMenuItemByIdAsync(Guid menuItemId);
        Task<BaseResponse<bool>> DeleteMenuItemAsync(Guid menuItemId);
        public Task<BaseResponse<MenuItem>> EditMenuItemAsync(string userToken, Guid menuItemId, EditMenuItemRequestModel request);
        public Task<BaseResponse<IList<MenuItemDto>>> GetAllMenuItemsByCompanyNameAsync(string businessName);
        public Task<BaseResponse<bool>> PublishMenuItem(string businessName);
    }
}
