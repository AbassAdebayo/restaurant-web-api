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
    public class ModifierItemService : IModifierItemService
    {
        private readonly IModifierGroupRepository _modifierGroupRepository;
        private readonly IModifierItemRepository _modifierItemRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<ModifierItemService> _logger;

        public ModifierItemService(IModifierGroupRepository modifierGroupRepository, IModifierItemRepository modifierItemRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<ModifierItemService> logger)
        {
            _modifierGroupRepository = modifierGroupRepository;
            _modifierItemRepository = modifierItemRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<ModifierItem>> AddModifierItemAsync(string userToken, Guid modifierGroupId, CreateModifierItemRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<ModifierItem>
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
                    return new BaseResponse<ModifierItem>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                } 
            }

            var getModifierGroup = await _modifierGroupRepository.GetModifierGroupByIdAsync(modifierGroupId);

            if (getModifierGroup == null)
            {
                _logger.LogError("The ModifierGroup accessed couldn't be fetched!");
                return new BaseResponse<ModifierItem>
                {
                    Message = "The ModifierGroup accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<ModifierItem>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var modifierItems = new List<ModifierItem>();
            var modifierItemsCount = 0; // Track the count of successfully added items.
           
            foreach (var modifierItem in request.ModifierItemNames)
            {
                var modifierItemExists = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? await _modifierItemRepository.ModifierItemExistsByNameAndCompanyName(modifierItem.ModifierItemName, user.BusinessName) : await _modifierItemRepository.ModifierItemExistsByNameAndCompanyName(modifierItem.ModifierItemName, user.CreatedBy);

                if (modifierItemExists)
                {
                    _logger.LogError("ModifierItem already exists!");
                    return new BaseResponse<ModifierItem>
                    {
                        Message = "ModifierItem already exists!",
                        Status = false
                    };
                }

                var item = new ModifierItem
                {
                    ModifierGroupId = getModifierGroup.Id,
                    ModifierItemName = modifierItem.ModifierItemName,
                    CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                    CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                    CreatedOn = DateTime.UtcNow,
                    Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft
                };

                var addModifierItem = await _modifierItemRepository.CreateModifierItem(item);

                if (addModifierItem is null)
                {
                    _logger.LogError("Item(s) couldn't be added to the group.");
                    return new BaseResponse<ModifierItem>
                    {
                        Message = "Item(s) couldn't be added to the group.",
                        Status = false
                    };
                }

                modifierItems.Add(addModifierItem);
                modifierItemsCount++; 
            }

            if (modifierItemsCount > 0)
            {
                _logger.LogInformation($"{modifierItemsCount} item(s) added to the group.");
                return new BaseResponse<ModifierItem>
                {
                    Message = $"{modifierItemsCount} item(s) added to the group.",
                    Status = true,
                    DataList = modifierItems
                };
            }
            else
            {
                _logger.LogWarning("No items were added to the group.");
                return new BaseResponse<ModifierItem>
                {
                    Message = "No items were added to the group.",
                    Status = false
                };
            }

        }

        public async Task<BaseResponse<bool>> DeleteModifierItemAsync(Guid modifierItemId)
        {
            var getModifierItem = await _modifierItemRepository.GetModifierItemByIdAsync(modifierItemId);
            if (getModifierItem is null)
            {
                _logger.LogError("ModifierItem does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "ModifierItem does not exist.",
                    Status = false
                };
            }

            var result = await _modifierItemRepository.DeleteModifierItem(getModifierItem);

            if (!result)
            {
                _logger.LogError("ModifierItem couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"ModifierItem couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("ModifierItem deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"ModifierItem deleted successfully",
                Status = true,

            };
        }

        public async Task<BaseResponse<ModifierItem>> EditModifierItemAsync(string userToken, Guid modifierItemId, EditModifierItemRequestModel request)
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
                    return new BaseResponse<ModifierItem>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            var existingModifierItem = await _modifierItemRepository.GetModifierItemByIdAsync(modifierItemId);
            if (existingModifierItem is null)
            {
                _logger.LogError("ModifierItem accessed doesnt exist.");
                return new BaseResponse<ModifierItem>
                {
                    Message = $"ModifierItem accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<ModifierItem>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            existingModifierItem.ModifierItemName = request.ModifierItemName;
            existingModifierItem.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingModifierItem.LastModifiedOn = DateTime.UtcNow;
            existingModifierItem.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
            existingModifierItem.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;

            var editedItem = await _modifierItemRepository.EditModifierItemAsync(existingModifierItem);

            if(editedItem is null)
            {
                _logger.LogError("ModifierItem update unsuccessful");
                return new BaseResponse<ModifierItem>
                {
                    Message = "ModifierItem update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("ModifierItem update succeeded");
            return new BaseResponse<ModifierItem>
            {
                Message = "ModifierItem update succeeded",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<ModifierItemDto>>> GetAllModifierItemsByCompanyNameAsync(string businessName)
        {
            var modifierItems = await _modifierItemRepository.GetAllModifierItemsByCompanyNameAsync(businessName);
            if(modifierItems  is null || !modifierItems.Any()) 
            {
                _logger.LogError("ModifierItems are empty, couldn't be fetched");
                return new BaseResponse<IList<ModifierItemDto>>
                {
                    Message = $"ModifierItems are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var modifierItemsDto = modifierItems.Select(mi => new ModifierItemDto
            {
                Id = mi.Id,
                ModifierItemName = mi.ModifierItemName,
                CompanyName = mi.CompanyName,
                CreatedBy = mi.CreatedBy,
                CreatedOn = mi.CreatedOn,
                LastModifiedOn = mi.LastModifiedOn,
                ModifierGroupId = mi.ModifierGroupId,
                Status = mi.Status

            }).ToList();

            return new BaseResponse<IList<ModifierItemDto>>
            {
                Message = "ModifierItems fetched successfully",
                Status = true,
                Data = modifierItemsDto
            };
        }

        public async Task<BaseResponse<ModifierItemDto>> GetModifierItemByIdAsync(Guid modifierItemId)
        {
            var getModifierItem = await _modifierItemRepository.GetModifierItemByIdAsync(modifierItemId);

            if(getModifierItem is null)
            {
                _logger.LogError("ModifierItem couldn't be fetched");
                return new BaseResponse<ModifierItemDto>
                {
                    Message = $"ModifierItem couldn't be fetched",
                    Status = false,

                };
            }

            var modifierItemDto = new ModifierItemDto
            {
                Id = getModifierItem.Id,
                ModifierItemName = getModifierItem.ModifierItemName,
                CompanyName = getModifierItem.CompanyName,
                CreatedBy = getModifierItem.CreatedBy,
                CreatedOn = getModifierItem.CreatedOn,
                LastModifiedOn = getModifierItem.LastModifiedOn,
                ModifierGroupId = getModifierItem.ModifierGroupId,
                Status = getModifierItem.Status
            };

            return new BaseResponse<ModifierItemDto>
            {
                Message = "ModifierItems fetched successfully",
                Status = true,
                Data = modifierItemDto
            };
        }

        public async Task<BaseResponse<bool>> PublishModifierItem(string businessName)
        {
            var modifierItems = await _modifierItemRepository.GetAllDraftModifierItemsByCompanyNameAsync(businessName);
            if (modifierItems is null || !modifierItems.Any())
            {
                _logger.LogError("No draft modifier items found to publish, but other entities are published.");
                return new BaseResponse<bool>
                {
                    Message = $"No draft modifier items found to publish, but other entities are published.",
                    Status = true,

                };
            }

            try
            {
                
                foreach (var modifierItem in modifierItems)
                {
                    modifierItem.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Published;

                    await _modifierItemRepository.EditModifierItemAsync(modifierItem);

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
                _logger.LogError("Error occurred while publishing modifier items.");
                return new BaseResponse<bool>
                {
                    Message = $"Error occurred while publishing modifier items",
                    Status = false,

                };
            }
                
              
            



        }
    }
}
