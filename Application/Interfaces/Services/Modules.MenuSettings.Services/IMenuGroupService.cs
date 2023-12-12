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
    public interface IMenuGroupService
    {
        public Task<BaseResponse<MenuGroup>> AddMenuGroupAsync(string userToken, Guid menuId, CreateMenuGroupRequestModel request);
        public Task<BaseResponse<MenuGroupDto>> GetMenuGroupByIdAsync(Guid menuGroupId);
        Task<BaseResponse<bool>> DeleteMenuGroupAsync(Guid menuGroupId);
        public Task<BaseResponse<MenuGroup>> EditMenuGroupAsync(string userToken, Guid menuGroupId, EditMenuGroupRequestModel request);
        public Task<BaseResponse<IList<MenuGroupDto>>> GetAllMenuGroupsByCompanyNameAsync(string businessName);
        public Task<BaseResponse<bool>> PublishMenuGroup(string businessName);
    }
}
