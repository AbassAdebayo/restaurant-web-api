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
    public interface IMenuService
    {
        public Task<BaseResponse<Menu>> AddMenuAsync(string userToken, CreateMenuRequestModel request);
        public Task<BaseResponse<MenuDto>> GetMenuByIdAsync(Guid menuId);
        Task<BaseResponse<bool>> DeleteMenuAsync(Guid menuId);
        public Task<BaseResponse<Menu>> EditMenuAsync(string userToken, Guid menuId, EditMenuRequestModel request);
        public Task<BaseResponse<IList<MenuDto>>> GetAllMenusByCompanyNameAsync(string businessName);
        public Task<BaseResponse<bool>> PublishMenu(string businessName);
    }
}
