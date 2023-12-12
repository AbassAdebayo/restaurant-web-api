using Application.DTOs.MenuSettingDtos;
using Application.DTOs;
using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Services
{
    public interface IModifierItemService
    {
        public Task<BaseResponse<ModifierItem>> AddModifierItemAsync(string userToken, Guid modifierGroupId, CreateModifierItemRequestModel request);
        public Task<BaseResponse<ModifierItemDto>> GetModifierItemByIdAsync(Guid modifierItemId);
        Task<BaseResponse<bool>> DeleteModifierItemAsync(Guid modifierItemId);
        public Task<BaseResponse<ModifierItem>> EditModifierItemAsync(string userToken, Guid modifierItemId, EditModifierItemRequestModel request);
        public Task<BaseResponse<IList<ModifierItemDto>>> GetAllModifierItemsByCompanyNameAsync(string businessName);
        public Task<BaseResponse<bool>> PublishModifierItem(string businessName);
    }
}
