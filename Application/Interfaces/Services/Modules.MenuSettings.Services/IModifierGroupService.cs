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
    public interface IModifierGroupService
    {
        public Task<BaseResponse<ModifierGroup>> AddModifierGroupAsync(string userToken, CreateModifierGroupRequestModel request);
        public Task<BaseResponse<ModifierGroupDto>> GetModifierGroupByIdAsync(Guid modifierGroupId);
        Task<BaseResponse<bool>> DeleteModifierGroupAsync(Guid modifierGroupId);
        public Task<BaseResponse<ModifierGroup>> EditModifierGroupAsync(string userToken, Guid modifierGroupId, EditModifierGroupRequestModel request);
        public Task<BaseResponse<IList<ModifierGroupDto>>> GetAllModifierGroupsByCompanyNameAsync(string businessName);
        public Task<BaseResponse<ModifierGroup>> ApplyModifierRulesToModifierGroupAsync(Guid modifierGroupId, ApplyModifierRulesToModifierGroupRequestModel request);
        public Task<BaseResponse<bool>> PublishModifierGroup(string businessName);
    }
}
