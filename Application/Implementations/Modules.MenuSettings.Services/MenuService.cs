using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Exceptions;
using Application.Implementations.Modules.RolePermission.Services;
using Application.Interfaces;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.MenuSettings.Repositories;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Domain.Domain.Modules.MenuSettings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.MenuSettings.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<MenuService> _logger;

        public MenuService(IMenuRepository menuRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<MenuService> logger)
        {
            _menuRepository = menuRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<Menu>> AddMenuAsync(string userToken, CreateMenuRequestModel request)
        {

            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<Menu>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            //if (!user.EmailConfirmed) throw new BadRequestException("User unverified");
            if(user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
               
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Menu Settings");
             
                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<Menu>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }
          
            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Menu>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var menuExists = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? await _menuRepository.MenuExistsByNameAndCompanyName(request.MenuName, user.BusinessName) : await _menuRepository.MenuExistsByNameAndCompanyName(request.MenuName, user.CreatedBy);
            if (menuExists)
            {
                _logger.LogError("Menu already exists!");
                return new BaseResponse<Menu>
                {
                    Message = "Menu already exists!!",
                    Status = false
                };
            }
            // Take the first 3 characters of the Menu name
            var menuNameAbbreviation = request.MenuName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuCode(menuNameAbbreviation);
            var menu = new Menu
            {
                MenuName = request.MenuName,
                Channel = request.Channel,
                TagName = request.TagName,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                MenuCode = uniqueCode,
                Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft,


            };  

            var menuCreated = await _menuRepository.CreateMenu(menu);

            if (menuCreated is null)
            {
                _logger.LogError("Menu creation couldn't be completed.");
                return new BaseResponse<Menu>
                {
                    Message = "Menu creation couldn't be completed.",
                    Status = false
                };
            }

            _logger.LogError("Menu has been created successfully.");
            return new BaseResponse<Menu>
            {
                Message = "Menu has been created successfully.",
                Status = true,
                Data = menuCreated
            };

        }

        public async Task<BaseResponse<bool>> DeleteMenuAsync(Guid menuId)
        {
            var getMenu = await _menuRepository.GetMenuByIdAsync(menuId);
            if(getMenu is null)
            {
                _logger.LogError("Menu does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "Menu does not exist.",
                    Status = false
                };
            }

            var result = await _menuRepository.DeleteMenu(getMenu);

            if (!result)
            {
                _logger.LogError("Menu couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"Menu couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("Menu deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"Menu deleted successfully",
                Status = true,

            };
            
        }

        public async Task<BaseResponse<Menu>> EditMenuAsync(string userToken, Guid menuId, EditMenuRequestModel request)
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
                    return new BaseResponse<Menu>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            var existingMenu = await _menuRepository.GetMenuByIdAsync(menuId);
            if(existingMenu is null)
            {
                _logger.LogError("Menu accessed doesnt exist.");
                return new BaseResponse<Menu>
                {
                    Message = $"Menu accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Menu>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            // Take the first 3 characters of the Menu name
            var menuNameAbbreviation = request.MenuName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuCode(menuNameAbbreviation);
            existingMenu.MenuName = request.MenuName;
            existingMenu.Channel = request.Channel;
            existingMenu.TagName = request.TagName;
            existingMenu.MenuCode = uniqueCode;
            existingMenu.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingMenu.LastModifiedOn = DateTime.UtcNow;
            existingMenu.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
            existingMenu.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;

            

            var updateMenu = await _menuRepository.EditMenuAsync(existingMenu);

            if(updateMenu is null)
            {
                _logger.LogError("Menu update unsuccessful");
                return new BaseResponse<Menu>
                {
                    Message = "Menu update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("Menu update succeeded");
            return new BaseResponse<Menu>
            {
                Message = "Menu update succeeded",
                Status = true
            };


        }

        public async Task<BaseResponse<IList<MenuDto>>> GetAllMenusByCompanyNameAsync(string businessName)
        {
            var menus = await _menuRepository.GetAllMenusByCompanyNameAsync(businessName);
            if (menus is null || !menus.Any())
            {
                _logger.LogError("Menus are empty, couldn't be fetched");
                return new BaseResponse<IList<MenuDto>>
                {
                    Message = $"Menus are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var menusDto = menus.Select(m => new MenuDto
            {
                MenuId = m.Id,
                MenuName = m.MenuName,
                Channel = m.Channel,
                TagName = m.TagName,
                CompanyName = m.CompanyName,
                CreatedOn = m.CreatedOn,
                CreatedBy = m.CreatedBy,
                MenuCode = m.MenuCode,
                Status = m.Status,
                MenuGroups = m.MenuGroups.Select(mg => new MenuGroupDto
                {
                    MenuGroupId = mg.Id,
                    MenuGroupName = mg.MenuGroupName,
                    MenuGroupCode = mg.MenuGroupCode,
                    MenuGroupImage = mg.MenuGroupImage,
                    Channel = mg.Channel,
                    TagName = mg.TagName,
                    CompanyName = mg.CompanyName,
                    MenuGroupPrice = mg.MenuGroupPrice,
                    MenuGroupPricingOption = mg.MenuGroupPricingOption,
                    MenuId = mg.MenuId,
                    CreatedBy = mg.CreatedBy,
                    CreatedOn = mg.CreatedOn,
                    Status = mg.Status,
                    MenuItems = mg.MenuItems.Select(mi => new MenuItemDto
                    {
                        MenuItemId = mi.Id,
                        MenuItemName = mi.MenuItemName,
                        MenuItemCode = mi.MenuItemCode,
                        TagName = mi.TagName,
                        Channel = mi.Channel,
                        MenuItemImage = mi.MenuItemImage,
                        BasePrice = mi.BasePrice,
                        CurrentPrice = mi.CurrentPrice,
                        MenuItemPricingOption = mi.MenuItemPricingOption,
                        MiniPriceRange = mi.MiniPriceRange,
                        MaxPriceRange = mi.MaxPriceRange,
                        CompanyName = mi.CompanyName,
                        CreatedBy = mi.CreatedBy,
                        CreatedOn = mi.CreatedOn,
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
                   

                }).ToList()
                

            }).ToList();

            return new BaseResponse<IList<MenuDto>>
            {
                Message = "Menus fetched successfully",
                Status = true,
                Data = menusDto
            };
        }

        public async Task<BaseResponse<MenuDto>> GetMenuByIdAsync(Guid menuId)
        {
            var menu = await _menuRepository.GetMenuByIdAsync(menuId);
            if (menu is null)
            {
                _logger.LogError("Menu couldn't be fetched");
                return new BaseResponse<MenuDto>
                {
                    Message = $"Menu couldn't be fetched",
                    Status = false,

                };
            }

            var menuDto = new MenuDto
            {
                MenuId = menu.Id,
                MenuName = menu.MenuName,
                Channel = menu.Channel,
                CompanyName = menu.CompanyName,
                CreatedOn = menu.CreatedOn,
                CreatedBy = menu.CreatedBy,
                MenuCode = menu.MenuCode,
                TagName = menu.TagName,
                Status = menu.Status,
                MenuGroups = menu.MenuGroups.Select(mg => new MenuGroupDto
                {
                    MenuGroupId = mg.Id,
                    MenuGroupName = mg.MenuGroupName,
                    MenuGroupCode = mg.MenuGroupCode,
                    MenuGroupImage = mg.MenuGroupImage,
                    Channel = mg.Channel,
                    CompanyName = mg.CompanyName,
                    MenuGroupPrice = mg.MenuGroupPrice,
                    MenuGroupPricingOption = mg.MenuGroupPricingOption,
                    MenuId = mg.MenuId,
                    CreatedBy = mg.CreatedBy,
                    CreatedOn = mg.CreatedOn,
                    TagName = mg.TagName,
                    Status = mg.Status,
                    MenuItems = mg.MenuItems.Select(mi => new MenuItemDto
                    {
                        MenuItemId = mi.Id,
                        MenuItemName = mi.MenuItemName,
                        MenuItemCode = mi.MenuItemCode,
                        TagName = mi.TagName,
                        Channel = mi.Channel,
                        MenuItemImage = mi.MenuItemImage,
                        BasePrice = mi.BasePrice,
                        CurrentPrice = mi.CurrentPrice,
                        MenuItemPricingOption = mi.MenuItemPricingOption,
                        MiniPriceRange = mi.MiniPriceRange,
                        MaxPriceRange = mi.MaxPriceRange,
                        CompanyName = mi.CompanyName,
                        CreatedBy = mi.CreatedBy,
                        CreatedOn = mi.CreatedOn,
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

                }).ToList()
            };

            _logger.LogError("Menu fetched successfully");
            return new BaseResponse<MenuDto>
            {
                Message = "Menu fetched successfully",
                Status = true,
                Data = menuDto
            };
        }

        public async Task<BaseResponse<bool>> PublishMenu(string businessName)
        {
            var menus = await _menuRepository.GetAllDraftMenusByCompanyNameAsync(businessName);

            if(menus is null)
            {
                _logger.LogError("No draft menus found to publish, but other entities are published.");
                return new BaseResponse<bool>
                {
                    Message = $"No draft menus found to publish, but other entities are published.",
                    Status = true,

                };
            }

            try
            {
                foreach (var menu in menus)
                {
                    menu.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Published;

                    await _menuRepository.EditMenuAsync(menu);

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
                _logger.LogError("Error occurred while publishing menus.");
                return new BaseResponse<bool>
                {
                    Message = "Error occurred while publishing menus.",
                    Status = false,

                };
            }
             
        }

        private string GenerateUniqueMenuCode(string abbreviation)
        {
            string baseCode = abbreviation + "-";
            int nextNumber = 1;

            // D3 formats the number with leading zeros
            string uniqueCode = baseCode + nextNumber.ToString("D3"); 

            var menuCodeExists =  _menuRepository.IsMenuCodeInUse(uniqueCode);

            // Check if the generated code is already in use
            while (menuCodeExists)
            {
                nextNumber++;
                uniqueCode = baseCode + nextNumber.ToString("D3");
            }

            return uniqueCode;
        }

    }
}
