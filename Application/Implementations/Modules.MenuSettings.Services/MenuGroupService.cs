using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Interfaces;
using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces.Modules.MenuSettings.Services;
using Domain.Domain.Modules.MenuSettings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.RolePermission.Repository;

namespace Application.Implementations.Modules.MenuSettings.Services
{
    public class MenuGroupService : IMenuGroupService
    {
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<MenuGroupService> _logger;
      

        public MenuGroupService(IMenuGroupRepository menuGroupRepository, IMenuRepository menuRepository, IIdentityService identityService, 
            IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, 
            ILogger<MenuGroupService> logger)
        {
            _menuGroupRepository = menuGroupRepository;
            _menuRepository = menuRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<MenuGroup>> AddMenuGroupAsync(string userToken, Guid menuId, CreateMenuGroupRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<MenuGroup>
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
                    return new BaseResponse<MenuGroup>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }
            var getMenu = await _menuRepository.GetMenuByIdAsync(menuId);
            if(getMenu == null)
            {
                _logger.LogError("The Menu accessed couldn't be fetched!");
                return new BaseResponse<MenuGroup>
                {
                    Message = "The Menu accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<MenuGroup>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var menuGroupExists = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? await _menuGroupRepository.MenuGroupExistsByNameAndCompanyName(request.MenuGroupName, user.BusinessName) : await _menuGroupRepository.MenuGroupExistsByNameAndCompanyName(request.MenuGroupName, user.CreatedBy);
            if (menuGroupExists)
            {
                _logger.LogError("MenuGroup already exists!");
                return new BaseResponse<MenuGroup>
                {
                    Message = "MenuGroup already exists!!",
                    Status = false

                };
            }

            // Take the first 3 characters of the Menu name
            var menuNameAbbreviation = request.MenuGroupName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuGroupCode(menuNameAbbreviation);

            var menuGroup = new MenuGroup
            {
                MenuId = getMenu.Id,
                MenuGroupName = request.MenuGroupName,
                Channel = request.Channel,
                TagName = request.TagName,
                MenuGroupCode = uniqueCode,
                MenuGroupImage = request.MenuGroupImage,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CreatedOn = DateTime.UtcNow,
                Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft,
                MenuGroupPricingOption = request.MenuGroupPricingOption,
                MenuGroupPrice = request.MenuGroupPricingOption ? (request.MenuGroupPrice > 0 ? request.MenuGroupPrice : throw new Exception("Price must be greater than zero.")) : 0,

            };
            var createMenuGroup = await _menuGroupRepository.CreateMenuGroup(menuGroup);

            if(createMenuGroup is null)
            {
                _logger.LogError("MenuGroup creation couldn't be completed.");
                return new BaseResponse<MenuGroup>
                {
                    Message = "MenuGroup creation couldn't be completed.",
                    Status = false
                };
            }

            _logger.LogError("MenuGroup has been created successfully.");
            return new BaseResponse<MenuGroup>
            {
                Message = "MenuGroup has been created successfully.",
                Status = true,
                Data = createMenuGroup
            };


        }

        public async Task<BaseResponse<bool>> DeleteMenuGroupAsync(Guid menuGroupId)
        {
            var getMenuGroup = await _menuGroupRepository.GetMenuGroupByIdAsync(menuGroupId);
            if (getMenuGroup is null)
            {
                _logger.LogError("MenuGroup does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "MenuGroup does not exist.",
                    Status = false
                };
            }

            var result = await _menuGroupRepository.DeleteMenuGroup(getMenuGroup);

            if (!result)
            {
                _logger.LogError("MenuGroup couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"MenuGroup couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("MenuGroup deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"MenuGroup deleted successfully",
                Status = true,

            };
        }

        public async Task<BaseResponse<MenuGroup>> EditMenuGroupAsync(string userToken, Guid menuGroupId, EditMenuGroupRequestModel request)
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
                    return new BaseResponse<MenuGroup>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            var existingMenuGroup = await _menuGroupRepository.GetMenuGroupByIdAsync(menuGroupId);
            if (existingMenuGroup is null)
            {
                _logger.LogError("MenuGroup accessed doesnt exist.");
                return new BaseResponse<MenuGroup>
                {
                    Message = $"MenuGroup accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<MenuGroup>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var menuNameAbbreviation = request.MenuGroupName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuGroupCode(menuNameAbbreviation);
            existingMenuGroup.MenuGroupName = request.MenuGroupName;
            existingMenuGroup.Channel = request.Channel;
            existingMenuGroup.TagName = request.TagName;
            existingMenuGroup.MenuGroupCode = uniqueCode;
            existingMenuGroup.MenuGroupImage = request.MenuGroupImage;
            existingMenuGroup.MenuGroupPricingOption = request.MenuGroupPricingOption;
            existingMenuGroup.MenuGroupPrice = request.MenuGroupPricingOption ? request.MenuGroupPrice : 0;
            existingMenuGroup.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingMenuGroup.LastModifiedOn = DateTime.UtcNow;
            existingMenuGroup.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
            existingMenuGroup.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;

            var updateMenuGroup = await _menuGroupRepository.EditMenuGroupAsync(existingMenuGroup);

            if (updateMenuGroup is null)
            {
                _logger.LogError("MenuGroup update unsuccessful");
                return new BaseResponse<MenuGroup>
                {
                    Message = "MenuGroup update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("MenuGroup update succeeded");
            return new BaseResponse<MenuGroup>
            {
                Message = "MenuGroup update succeeded",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<MenuGroupDto>>> GetAllMenuGroupsByCompanyNameAsync(string businessName)
        {
            var menuGroups = await _menuGroupRepository.GetAllMenuGroupsByCompanyNameAsync(businessName);
            if (menuGroups is null || !menuGroups.Any())
            {
                _logger.LogError("MenuGroups are empty, couldn't be fetched");
                return new BaseResponse<IList<MenuGroupDto>>
                {
                    Message = $"MenuGroups are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var menuGroupsDto = menuGroups.Select(mg => new MenuGroupDto
            {
                MenuGroupId = mg.Id,
                MenuGroupName = mg.MenuGroupName,
                Channel = mg.Channel,
                MenuGroupCode = mg.MenuGroupCode,
                MenuGroupImage = mg.MenuGroupImage,
                TagName = mg.TagName,
                MenuGroupPricingOption = mg.MenuGroupPricingOption,
                MenuGroupPrice = mg.MenuGroupPrice,
                MenuId = mg.MenuId,
                Menu = mg.Menu,
                CompanyName = mg.CompanyName,
                CreatedBy = mg.CreatedBy,
                CreatedOn = mg.CreatedOn,
                Status = mg.Status,
                MenuItems = mg.MenuItems.Select(mi => new MenuItemDto
                {
                    MenuItemId = mi.Id,
                    MenuItemName = mi.MenuItemName,
                    MenuItemImage = mi.MenuItemImage,
                    TagName = mi.TagName,
                    MenuItemCode = mi.MenuItemCode,
                    MiniPriceRange = mi.MiniPriceRange,
                    MaxPriceRange = mi.MaxPriceRange,
                    BasePrice = mi.BasePrice,
                    CurrentPrice = mi.CurrentPrice,
                    MenuItemPricingOption = mi.MenuItemPricingOption,
                    Status = mi.Status,
                    CompanyName = mi.CompanyName,
                    MenuGroupId = mi.MenuGroupId,
                    

                }).ToList()

            }).ToList();

            return new BaseResponse<IList<MenuGroupDto>>
            {
                Message = "MenuGroups fetched successfully",
                Status = true,
                Data = menuGroupsDto
            };
        }

        public async Task<BaseResponse<MenuGroupDto>> GetMenuGroupByIdAsync(Guid menuGroupId)
        {
            var menuGroup = await _menuGroupRepository.GetMenuGroupByIdAsync(menuGroupId);
            if (menuGroup is null)
            {
                _logger.LogError("MenuGroup couldn't be fetched");
                return new BaseResponse<MenuGroupDto>
                {
                    Message = $"MenuGroup couldn't be fetched",
                    Status = false,

                };
            }

            var menuGroupDto = new MenuGroupDto
            {
                MenuGroupId = menuGroup.Id,
                MenuGroupName = menuGroup.MenuGroupName,
                Channel = menuGroup.Channel,
                MenuGroupCode = menuGroup.MenuGroupCode,
                MenuGroupImage = menuGroup.MenuGroupImage,
                TagName = menuGroup.TagName,
                MenuGroupPricingOption = menuGroup.MenuGroupPricingOption,
                MenuGroupPrice = menuGroup.MenuGroupPrice,
                MenuId = menuGroup.MenuId,
                Menu = menuGroup.Menu,
                CompanyName = menuGroup.CompanyName,
                CreatedBy = menuGroup.CreatedBy,
                CreatedOn = menuGroup.CreatedOn,
                Status = menuGroup.Status,
                MenuItems = menuGroup.MenuItems.Select(mi => new MenuItemDto
                {
                    MenuItemId = mi.Id,
                    MenuItemName = mi.MenuItemName,
                    MenuItemImage = mi.MenuItemImage,
                    TagName = mi.TagName,
                    MenuItemCode = mi.MenuItemCode,
                    MiniPriceRange = mi.MiniPriceRange,
                    MaxPriceRange = mi.MaxPriceRange,
                    BasePrice = mi.BasePrice,
                    CurrentPrice = mi.CurrentPrice,
                    MenuItemPricingOption = mi.MenuItemPricingOption,
                    CompanyName = mi.CompanyName,
                    MenuGroupId = mi.MenuGroupId,
                    Status = mi.Status,
                    TimeSpecificPrice = mi.TimeSpecificPrice.Select(ts => new TimeSpecificPriceOptionDto
                    {
                        Id = ts.Id,
                        BasePrice = ts.BasePrice,
                        DiscountedPrice = ts.DiscountedPrice,
                        StartDateTime = ts.StartDateTime,
                        EndDateTime = ts.EndDateTime,
                        MenuItemId = ts.MenuItemId
                    }).ToList(),
                    RangePrices = mi.RangePrices.Select(rp => new RangePriceOptionDto
                    {
                        Id = rp.Id,
                        MenuItemId = rp.MenuItemId,
                        Name = rp.Name,
                        Price = rp.Price
                    }).ToList()


                }).ToList()
            };

            _logger.LogError("MenuGroups fetched successfully");
            return new BaseResponse<MenuGroupDto>
            {
                Message = "Menus fetched successfully",
                Status = true,
                Data = menuGroupDto
            };
        }

        public async Task<BaseResponse<bool>> PublishMenuGroup(string businessName)
        {
            var draftMenuGroups = await _menuGroupRepository.GetAllDraftMenuGroupsByCompanyNameAsync(businessName);
            if (draftMenuGroups is null)
            {
                _logger.LogError("No draft menu groups found to publish, but other entities are published");
                return new BaseResponse<bool>
                {
                    Message = $"No draft menu groups found to publish, but other entities are published",
                    Status = true,

                };
            }

            try
            {
                foreach (var menuGroup in draftMenuGroups)
                {
                    menuGroup.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Published;

                    await _menuGroupRepository.EditMenuGroupAsync(menuGroup);

                }
                _logger.LogInformation("Publish succeeded!");
                return new BaseResponse<bool>
                {
                    Message = "Publish succeeded!",
                    Status = true,
                };
            }
            catch
            {
                _logger.LogError("Error occurred while publishing menu groups");
                return new BaseResponse<bool>
                {
                    Message = $"Error occurred while publishing menu groups",
                    Status = false,
                };
            }

        }

        private string GenerateUniqueMenuGroupCode(string abbreviation)
        {
            string baseCode = abbreviation + "-";
            int nextNumber = 1;

            // D3 formats the number with leading zeros
            string uniqueCode = baseCode + nextNumber.ToString("D3");

            var menuGroupCodeExists = _menuGroupRepository.IsMenuGroupCodeInUse(uniqueCode);

            // Check if the generated code is already in use
            while (menuGroupCodeExists)
            {
                nextNumber++;
                uniqueCode = baseCode + nextNumber.ToString("D3");
            }

            return uniqueCode;
        }
    }
}
