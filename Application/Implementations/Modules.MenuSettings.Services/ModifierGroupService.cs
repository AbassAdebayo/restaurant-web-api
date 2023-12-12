using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Interfaces;
using Domain.Domain.Modules.MenuSettings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.RolePermission.Repository;

namespace Application.Implementations.Modules.MenuSettings.Services
{
    public class ModifierGroupService : IModifierGroupService
    {
        private readonly IModifierGroupRepository _modifierGroupRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<ModifierGroupService> _logger;

        public ModifierGroupService(IModifierGroupRepository modifierGroupRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<ModifierGroupService> logger)
        {
            _modifierGroupRepository = modifierGroupRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ModifierGroup>> AddModifierGroupAsync(string userToken, CreateModifierGroupRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            //if (!user.EmailConfirmed) throw new BadRequestException("User unverified");
            if (user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Menu Settings");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<ModifierGroup>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var modifierGroupExists = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? await _modifierGroupRepository.ModifierGroupExistsByNameAndCompanyName(request.ModifierGroupName, user.BusinessName) : await _modifierGroupRepository.ModifierGroupExistsByNameAndCompanyName(request.ModifierGroupName, user.CreatedBy);
            if (modifierGroupExists)
            {
                _logger.LogError("ModifierGroup already exists!");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "ModifierGroup already exists!!",
                    Status = false

                };
            }

            var modifierGroup = new ModifierGroup
            {
                ModifierGroupName = request.ModifierGroupName,
                ModifierGroupPrice = request.ModifierGroupPrice,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CreatedOn = DateTime.UtcNow,
                Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft
            };
            var newModifierGroup = await _modifierGroupRepository.CreateModifierGroup(modifierGroup);

            if(newModifierGroup is null)
            {
                _logger.LogError("ModifierGroup creation couldn't be completed.");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "ModifierGroup creation couldn't be completed.",
                    Status = false
                };
            }

            _logger.LogError("ModifierGroup has been created successfully.");
            return new BaseResponse<ModifierGroup>
            {
                Message = "ModifierGroup has been created successfully.",
                Status = true,
                Data = newModifierGroup
            };
        }

        public async Task<BaseResponse<ModifierGroup>> ApplyModifierRulesToModifierGroupAsync(Guid modifierGroupId, ApplyModifierRulesToModifierGroupRequestModel request)
        {
            var getModifierGroup = await _modifierGroupRepository.GetModifierGroupByIdAsync(modifierGroupId);

            if (getModifierGroup is null)
            {
                _logger.LogError("ModifierGroup does not exist.");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "ModifierGroup does not exist.",
                    Status = false
                };
            }

            foreach (var selectedRule in request.SelectedRules)
            {
                string ruleDescription = string.Empty;

                switch (selectedRule)
                {
                    case Domain.Domain.Modules.Users.Entities.Enums.ModifierRules.Servers_must_make_a_selection_for_this_group:
                        ruleDescription = "Servers must make a selection for this group\n";
                        break;

                    case Domain.Domain.Modules.Users.Entities.Enums.ModifierRules.This_group_is_optional_and_is_shown_on_add:
                        ruleDescription = "This group is optional and is shown on add\n";
                        break;

                    case Domain.Domain.Modules.Users.Entities.Enums.ModifierRules.This_group_is_optional_and_is_not_shown_on_add:
                        ruleDescription = "This group is optional and is not shown on add\n";
                        break;

                    case Domain.Domain.Modules.Users.Entities.Enums.ModifierRules.More_than_modifier_can_be_chosen:
                        ruleDescription = "More than one modifier can be chosen\n";
                        break;

                    case Domain.Domain.Modules.Users.Entities.Enums.ModifierRules.Only_one_modifier_can_be_chosen:
                        ruleDescription = "Only one modifier can be chosen\n";
                        break;

                    default:
                        break;
                }

                getModifierGroup.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;
                getModifierGroup.RuleDescription += ruleDescription;
            }

            await _modifierGroupRepository.EditModifierGroupAsync(getModifierGroup);

            _logger.LogInformation("Rule(s) applied successfully.");
            return new BaseResponse<ModifierGroup>
            {
                Message = "Rule(s) applied successfully.",
                Status = true,
            };
        }



        public async Task<BaseResponse<bool>> DeleteModifierGroupAsync(Guid modifierGroupId)
        {
            var getModifierGroup = await _modifierGroupRepository.GetModifierGroupByIdAsync(modifierGroupId);
            if (getModifierGroup is null)
            {
                _logger.LogError("ModifierGroup does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "ModifierGroup does not exist.",
                    Status = false
                };
            }

            var result = await _modifierGroupRepository.DeleteModifierGroup(getModifierGroup);

            if (!result)
            {
                _logger.LogError("ModifierGroup couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"ModifierGroup couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("ModifierGroup deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"ModifierGroup deleted successfully",
                Status = true,

            };
        }

        public async Task<BaseResponse<ModifierGroup>> EditModifierGroupAsync(string userToken, Guid modifierGroupId, EditModifierGroupRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user.UserType != Domain.Entities.Enums.UserType.SuperAdmin)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Menu Settings");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<ModifierGroup>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            var existingModifierGroup = await _modifierGroupRepository.GetModifierGroupByIdAsync(modifierGroupId);
            if (existingModifierGroup is null)
            {
                _logger.LogError("ModifierGroup accessed doesnt exist.");
                return new BaseResponse<ModifierGroup>
                {
                    Message = $"ModifierGroup accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }
            existingModifierGroup.ModifierGroupName = request.ModifierGroupName;
            existingModifierGroup.ModifierGroupPrice = request.ModifierGroupPrice;
            existingModifierGroup.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingModifierGroup.LastModifiedOn = DateTime.UtcNow;
            existingModifierGroup.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
            existingModifierGroup.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;

            var updateModifierGroup = await _modifierGroupRepository.EditModifierGroupAsync(existingModifierGroup);

            if(updateModifierGroup is null)
            {
                _logger.LogError("ModifierGroup update unsuccessful");
                return new BaseResponse<ModifierGroup>
                {
                    Message = "ModifierGroup update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("ModifierGroup update succeeded");
            return new BaseResponse<ModifierGroup>
            {
                Message = "ModifierGroup update succeeded",
                Status = true
            };
        }   

        public async Task<BaseResponse<IList<ModifierGroupDto>>> GetAllModifierGroupsByCompanyNameAsync(string businessName)
        {
            var modifierGroups = await _modifierGroupRepository.GetAllModifierGroupsByCompanyNameAsync(businessName);
            if (modifierGroups is null || !modifierGroups.Any())
            {
                _logger.LogError("ModifierGroups are empty, couldn't be fetched");
                return new BaseResponse<IList<ModifierGroupDto>>
                {
                    Message = $"ModifierGroups are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var modifierGroupsDto = modifierGroups.Select(mg => new ModifierGroupDto
            {
                Id = mg.Id,
                ModifierGroupName = mg.ModifierGroupName,
                ModifierGroupPrice = mg.ModifierGroupPrice,
                CompanyName = mg.CompanyName,
                RuleDescription = mg.RuleDescription,
                Status = mg.Status,
                CreatedBy = mg.CreatedBy,
                CreatedOn = mg.CreatedOn,
                LastModifiedOn = mg.LastModifiedOn,
                ModifierItems = mg.ModifierItems.Select(mi => new ModifierItemDto
                {
                    Id = mi.Id,
                    ModifierItemName = mi.ModifierItemName,
                    CompanyName = mi.CompanyName,
                    Status = mi.Status,
                    CreatedBy = mi.CreatedBy,
                    CreatedOn = mi.CreatedOn,
                    LastModifiedOn = mi.LastModifiedOn
                }).ToList()
            }).ToList();

            return new BaseResponse<IList<ModifierGroupDto>>
            {
                Message = "ModifierGroups fetched successfully",
                Status = true,
                Data = modifierGroupsDto
            };
        }

        public async Task<BaseResponse<ModifierGroupDto>> GetModifierGroupByIdAsync(Guid modifierGroupId)
        {
            var modifierGroup = await _modifierGroupRepository.GetModifierGroupByIdAsync(modifierGroupId);
            if (modifierGroup is null)
            {
                _logger.LogError("ModifierGroup couldn't be fetched");
                return new BaseResponse<ModifierGroupDto>
                {
                    Message = $"ModifierGroup couldn't be fetched",
                    Status = false,

                };
            }

            var modifierGroupDto = new ModifierGroupDto
            {
                Id = modifierGroup.Id,
                ModifierGroupName = modifierGroup.ModifierGroupName,
                ModifierGroupPrice = modifierGroup.ModifierGroupPrice,
                RuleDescription = modifierGroup.RuleDescription,
                CompanyName = modifierGroup.CompanyName,
                CreatedBy = modifierGroup.CreatedBy,
                CreatedOn = modifierGroup.CreatedOn,
                LastModifiedOn = modifierGroup.LastModifiedOn,
                Status = modifierGroup.Status,
                ModifierItems = modifierGroup.ModifierItems.Select(mi => new ModifierItemDto
                {
                    Id = mi.Id,
                    ModifierItemName = mi.ModifierItemName,
                    CompanyName = mi.CompanyName,
                    CreatedBy = mi.CreatedBy,
                    CreatedOn = mi.CreatedOn,
                    LastModifiedOn = mi.LastModifiedOn,
                    Status = mi.Status
                }).ToList(),
              
            };

            _logger.LogError("ModifierGroup fetched successfully");
            return new BaseResponse<ModifierGroupDto>
            {
                Message = "ModifierGroup fetched successfully",
                Status = true,
                Data = modifierGroupDto
            };
        }

        public async Task<BaseResponse<bool>> PublishModifierGroup(string businessName)
        {
            var modifierGroups = await _modifierGroupRepository.GetAllDraftModifierGroupsByCompanyNameAsync(businessName);

            if (modifierGroups is null || !modifierGroups.Any())
            {
                _logger.LogError("No draft modifier groups found to publish, but other entities are published.");
                return new BaseResponse<bool>
                {
                    Message = $"No draft modifier groups found to publish, but other entities are published.",
                    Status = true,

                };
            }
            try
            {
                foreach (var modifierGroup in modifierGroups)
                {
                    modifierGroup.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Published;

                    await _modifierGroupRepository.EditModifierGroupAsync(modifierGroup);

                }
                _logger.LogInformation("Publish succeeded!");
                return new BaseResponse<bool>
                {
                    Message = $"Publish succeeded!",
                    Status = true,

                };
            }
            catch
            {
                _logger.LogError("Error occurred while publishing modifier group");
                return new BaseResponse<bool>
                {
                    Message = $"Error occurred while publishing modifier group",
                    Status = false,

                };
            }
                
            
            



        }
    }
}
