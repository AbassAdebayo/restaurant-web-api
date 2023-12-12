using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
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
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<MenuItemService> _logger;

        public MenuItemService(IMenuGroupRepository menuGroupRepository, IMenuItemRepository menuItemRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<MenuItemService> logger)
        {
            _menuGroupRepository = menuGroupRepository;
            _menuItemRepository = menuItemRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<MenuItem>> AddMenuItemAsync(string userToken, Guid menuGroupId, CreateMenuItemRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<MenuItem>
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
                    return new BaseResponse<MenuItem>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            var getMenuGroup = await _menuGroupRepository.GetMenuGroupByIdAsync(menuGroupId);

            if (getMenuGroup == null)
            {
                _logger.LogError("The MenuGroup accessed couldn't be fetched!");
                return new BaseResponse<MenuItem>
                {
                    Message = "The MenuGroup accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<MenuItem>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var menuItemExists = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? await _menuItemRepository.MenuItemExistsByNameAndCompanyName(request.MenuItemName, user.BusinessName) : await _menuItemRepository.MenuItemExistsByNameAndCompanyName(request.MenuItemName, user.CreatedBy);
            if (menuItemExists)
            {
                _logger.LogError("MenuItem already exists!");
                return new BaseResponse<MenuItem>
                {
                    Message = "MenuItem already exists!",
                    Status = false

                };
            }

            // Take the first 3 characters of the Menu name
            var menuNameAbbreviation = request.MenuItemName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuItemCode(menuNameAbbreviation);

            var menuItem = new MenuItem
            {
                MenuGroupId = getMenuGroup.Id,
                MenuItemName = request.MenuItemName,
                MenuItemCode = uniqueCode,
                MenuGroupCode = getMenuGroup.MenuGroupCode,
                TagName = request.TagName,
                Channel = request.Channel,
                MenuItemImage = request.MenuItemImage,
                MenuItemPricingOption = request.MenuItemPricingOption,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CreatedOn = DateTime.UtcNow,
                Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft
            };
            if (getMenuGroup.MenuGroupPricingOption)
            {
                menuItem.BasePrice = getMenuGroup.MenuGroupPrice;
                menuItem.CurrentPrice = getMenuGroup.MenuGroupPrice;
                menuItem.MiniPriceRange = null;
                menuItem.MaxPriceRange = null;

            }
            else
            {
                if (menuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.BasePrice)
                {
                    menuItem.BasePrice = request.BasePrice > 0 ? request.BasePrice : throw new Exception("Price must be greater than zero");
                    menuItem.CurrentPrice = menuItem.BasePrice;
                    menuItem.MiniPriceRange = null;
                    menuItem.MaxPriceRange = null;
                }
                else if (menuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.RangePriceOption)
                {
                    menuItem.BasePrice = 0;
                    menuItem.CurrentPrice = 0;

                    decimal minPrice = request.RangePrices.Min(rp => rp.Price);
                    decimal maxPrice = request.RangePrices.Max(rp => rp.Price);

                    menuItem.MiniPriceRange = minPrice;
                    menuItem.MaxPriceRange = maxPrice;

                    foreach (var rangePrice in request.RangePrices)
                    {
                        var price = new RangePriceOption
                        {
                            MenuItemId = menuItem.Id,
                            Name = rangePrice.Name,
                            Price = rangePrice.Price,

                        };

                        menuItem.RangePrices.Add(price);
                    }


                }
                else if (menuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.TimeSpecificPriceOption)
                {
                    menuItem.BasePrice = 0;

                    foreach (var timeSpecificPrice in request.TimeSpecificPrice)
                    {
                        var price = new TimeSpecificPriceOption
                        {
                            MenuItemId = menuItem.Id,
                            BasePrice = timeSpecificPrice.BasePrice,
                            DiscountedPrice = timeSpecificPrice.DiscountedPrice,
                            StartDateTime = timeSpecificPrice.StartDateTime,
                            EndDateTime = timeSpecificPrice.EndDateTime,

                        };
                        menuItem.TimeSpecificPrice.Add(price);

                        if (timeSpecificPrice.StartDateTime <= DateTime.UtcNow && timeSpecificPrice.EndDateTime >= DateTime.UtcNow)
                        {
                            menuItem.CurrentPrice = timeSpecificPrice.DiscountedPrice;
                        }
                        else
                        {
                            menuItem.CurrentPrice = timeSpecificPrice.BasePrice;
                        }
                    }

                }
            }
            var newMenuItem = await _menuItemRepository.CreateMenuItem(menuItem);
            if (newMenuItem is null)
            {
                _logger.LogError("MenuItem creation couldn't be completed.");
                return new BaseResponse<MenuItem>
                {
                    Message = "MenuItem creation couldn't be completed.",
                    Status = false
                };
            }

            _logger.LogError("MenuItem has been created successfully.");
            return new BaseResponse<MenuItem>
            {
                Message = "MenuItem has been created successfully.",
                Status = true,
                Data = newMenuItem
            };
        }

        public async Task<BaseResponse<bool>> DeleteMenuItemAsync(Guid menuItemId)
        {
            var getMenuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (getMenuItem is null)
            {
                _logger.LogError("MenuItem does not exist.");
                return new BaseResponse<bool>
                {
                    Message = "MenuItem does not exist.",
                    Status = false
                };
            }

            var result = await _menuItemRepository.DeleteMenuItem(getMenuItem);

            if (!result)
            {
                _logger.LogError("MenuItem couldn't be deleted.");
                return new BaseResponse<bool>
                {
                    Message = $"MenuItem couldn't be deleted.",
                    Status = false,

                };
            }

            _logger.LogError("MenuItem deleted successfully.");
            return new BaseResponse<bool>
            {
                Message = $"MenuItem deleted successfully",
                Status = true,

            };
        }

        public async Task<BaseResponse<MenuItem>> EditMenuItemAsync(string userToken, Guid menuItemId, EditMenuItemRequestModel request)
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
                    return new BaseResponse<MenuItem>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            var existingMenuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (existingMenuItem is null)
            {
                _logger.LogError("MenuItem accessed doesnt exist.");
                return new BaseResponse<MenuItem>
                {
                    Message = $"MenuItem accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<MenuItem>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            var menuNameAbbreviation = request.MenuItemName.Substring(0, 3).ToUpper();
            var uniqueCode = GenerateUniqueMenuItemCode(menuNameAbbreviation);
            existingMenuItem.MenuItemName = request.MenuItemName;
            existingMenuItem.Channel = request.Channel;
            existingMenuItem.TagName = request.TagName;
            existingMenuItem.MenuItemCode = uniqueCode;
            existingMenuItem.MenuGroupCode = existingMenuItem.MenuGroupCode;
            existingMenuItem.MenuItemImage = request.MenuItemImage;
            existingMenuItem.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingMenuItem.LastModifiedOn = DateTime.UtcNow;
            existingMenuItem.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
            existingMenuItem.MenuItemPricingOption = request.MenuItemPricingOption;
            existingMenuItem.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Draft;

            if (existingMenuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.BasePrice)
            {
                existingMenuItem.BasePrice = request.BasePrice > 0 ? request.BasePrice : throw new Exception("Price must be greater than zero");
                existingMenuItem.CurrentPrice = existingMenuItem.BasePrice;
                existingMenuItem.MiniPriceRange = null;
                existingMenuItem.MaxPriceRange = null;
            }
            else if (existingMenuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.RangePriceOption)
            {
                existingMenuItem.BasePrice = 0;
                existingMenuItem.CurrentPrice = 0;

                decimal minPrice = request.RangePrices.Min(rp => rp.Price);
                decimal maxPrice = request.RangePrices.Max(rp => rp.Price);

                existingMenuItem.MiniPriceRange = minPrice;
                existingMenuItem.MaxPriceRange = maxPrice;

                existingMenuItem.RangePrices.Clear();

                foreach (var rangePrice in request.RangePrices)
                {
                    var price = new RangePriceOption
                    {
                        MenuItemId = existingMenuItem.Id,
                        Name = rangePrice.Name,
                        Price = rangePrice.Price,

                    };

                    existingMenuItem.RangePrices.Add(price);
                }
            }
            else if (existingMenuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.TimeSpecificPriceOption)
            {
                existingMenuItem.BasePrice = 0;

                existingMenuItem.TimeSpecificPrice.Clear();

                foreach (var timeSpecificPrice in request.TimeSpecificPrice)
                {
                    var price = new TimeSpecificPriceOption
                    {
                        MenuItemId = existingMenuItem.Id,
                        BasePrice = timeSpecificPrice.BasePrice,
                        DiscountedPrice = timeSpecificPrice.DiscountedPrice,
                        StartDateTime = timeSpecificPrice.StartDateTime,
                        EndDateTime = timeSpecificPrice.EndDateTime,

                    };
                    existingMenuItem.TimeSpecificPrice.Add(price);

                    if (timeSpecificPrice.StartDateTime <= DateTime.UtcNow && timeSpecificPrice.EndDateTime >= DateTime.UtcNow)
                    {
                        existingMenuItem.CurrentPrice = timeSpecificPrice.DiscountedPrice;
                    }
                    else
                    {
                        existingMenuItem.CurrentPrice = timeSpecificPrice.BasePrice;
                    }
                }
            }
            var editedMenuItem = await _menuItemRepository.EditMenuItemAsync(existingMenuItem);

            if (editedMenuItem is null)
            {
                _logger.LogError("MenuItem update unsuccessful");
                return new BaseResponse<MenuItem>
                {
                    Message = "MenuItem update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("MenuItem update succeeded");
            return new BaseResponse<MenuItem>
            {
                Message = "MenuItem update succeeded",
                Status = true
            };
        }

        public async Task<BaseResponse<IList<MenuItemDto>>> GetAllMenuItemsByCompanyNameAsync(string businessName)
        {

            var menuItems = await _menuItemRepository.GetAllMenuItemsByCompanyNameAsync(businessName);
            if (menuItems is null || !menuItems.Any())
            {
                _logger.LogError("MenuItems are empty, couldn't be fetched");
                return new BaseResponse<IList<MenuItemDto>>
                {
                    Message = $"MenuItems are empty, or couldn't be fetched",
                    Status = false,

                };
            }
            var menuItemsDto = menuItems.Select(mg => new MenuItemDto
            {
                MenuItemId = mg.Id,
                MenuGroupId = mg.MenuGroupId,
                MenuItemName = mg.MenuItemName,
                Channel = mg.Channel,
                MenuGroupCode = mg.MenuGroupCode,
                MenuItemCode = mg.MenuItemCode,
                MenuItemImage = mg.MenuItemImage,
                TagName = mg.TagName,
                MenuItemPricingOption = mg.MenuItemPricingOption,
                BasePrice = mg.BasePrice,
                CurrentPrice = mg.CurrentPrice,
                MaxPriceRange = mg.MaxPriceRange,
                MiniPriceRange = mg.MiniPriceRange,
                CompanyName = mg.CompanyName,
                CreatedBy = mg.CreatedBy,
                CreatedOn = mg.CreatedOn,
                Status = mg.Status,
                RangePrices = mg.RangePrices.Select(rp => new RangePriceOptionDto
                {
                    Id = rp.Id,
                    Name = rp.Name,
                    Price = rp.Price,
                    MenuItemId = rp.MenuItemId,

                }).ToList(),
                TimeSpecificPrice = mg.TimeSpecificPrice.Select(ts => new TimeSpecificPriceOptionDto
                {
                    Id = ts.Id,
                    MenuItemId = ts.MenuItemId,
                    BasePrice = ts.BasePrice,
                    DiscountedPrice = ts.DiscountedPrice,
                    StartDateTime = ts.StartDateTime,
                    EndDateTime = ts.EndDateTime,
                }).ToList()

             }).ToList();

            return new BaseResponse<IList<MenuItemDto>>
            {
                Message = "MenuItems fetched successfully",
                Status = true,
                Data = menuItemsDto
            };
        }


        public async Task<BaseResponse<MenuItemDto>> GetMenuItemByIdAsync(Guid menuItemId)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem is null)
            {
                _logger.LogError("MenuItem couldn't be fetched");
                return new BaseResponse<MenuItemDto>
                {
                    Message = $"MenuItem couldn't be fetched",
                    Status = false,

                };
            }

            var menuItemDto = new MenuItemDto
            {
                MenuItemId = menuItem.Id,
                MenuGroupId = menuItem.MenuGroupId,
                MenuItemName = menuItem.MenuItemName,
                Channel = menuItem.Channel,
                MenuGroupCode = menuItem.MenuGroupCode,
                MenuItemCode = menuItem.MenuItemCode,
                MenuItemImage = menuItem.MenuItemImage,
                TagName = menuItem.TagName,
                MenuItemPricingOption = menuItem.MenuItemPricingOption,
                BasePrice = menuItem.BasePrice,
                CurrentPrice = menuItem.CurrentPrice,
                MaxPriceRange = menuItem.MaxPriceRange,
                MiniPriceRange = menuItem.MiniPriceRange,
                CompanyName = menuItem.CompanyName,
                CreatedBy = menuItem.CreatedBy,
                CreatedOn = menuItem.CreatedOn,
                Status = menuItem.Status,
                RangePrices = menuItem.RangePrices.Select(rp => new RangePriceOptionDto
                {
                    Id = rp.Id,
                    Name = rp.Name,
                    Price = rp.Price,
                    MenuItemId = rp.MenuItemId,

                }).ToList(),
                TimeSpecificPrice = menuItem.TimeSpecificPrice.Select(ts => new TimeSpecificPriceOptionDto
                {
                    Id = ts.Id,
                    MenuItemId = ts.MenuItemId,
                    BasePrice = ts.BasePrice,
                    DiscountedPrice = ts.DiscountedPrice,
                    StartDateTime = ts.StartDateTime,
                    EndDateTime = ts.EndDateTime,
                }).ToList()

            };

            _logger.LogError("Menus fetched successfully");
            return new BaseResponse<MenuItemDto>
            {
                Message = "Menus fetched successfully",
                Status = true,
                Data = menuItemDto
            };
        }

        public async Task<BaseResponse<bool>> PublishMenuItem(string businessName)
        {
            var draftMenuItems = await _menuItemRepository.GetAllDraftMenuItemsByCompanyNameAsync(businessName);
            if (draftMenuItems is null)
            {
                _logger.LogError("No draft menu items found to publish, but other entities are published.");
                return new BaseResponse<bool>
                {
                    Message = $"No draft menu items found to publish, but other entities are published.",
                    Status = true,

                };
            }

            try
            {
                foreach (var menuItem in draftMenuItems)
                {
                    menuItem.Status = Domain.Domain.Modules.Users.Entities.Enums.EntityStatus.Published;

                    await _menuItemRepository.EditMenuItemAsync(menuItem);

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
                _logger.LogError("Error occurred while publishing menu items");
                return new BaseResponse<bool>
                {
                    Message = $"Error occurred while publishing menu items",
                    Status = false,

                };
            }

           

        }

        private string GenerateUniqueMenuItemCode(string abbreviation)
        {
            string baseCode = abbreviation + "-";
            int nextNumber = 1;

            // D3 formats the number with leading zeros
            string uniqueCode = baseCode + nextNumber.ToString("D3");

            var menuItemCodeExists = _menuItemRepository.IsMenuItemCodeInUse(uniqueCode);

            // Check if the generated code is already in use
            while (menuItemCodeExists)
            {
                nextNumber++;
                uniqueCode = baseCode + nextNumber.ToString("D3");
            }

            return uniqueCode;
        }
    }
}
