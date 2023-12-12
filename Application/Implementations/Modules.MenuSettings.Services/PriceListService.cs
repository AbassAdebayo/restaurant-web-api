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
using Domain.Domain.Modules.Users.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.MenuSettings.Services
{
    public class PriceListService : IPriceListService
    {
        private readonly IMenuGroupRepository _menuGroupRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<PriceListService> _logger;

        public PriceListService(IMenuGroupRepository menuGroupRepository, IMenuItemRepository menuItemRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<PriceListService> logger)
        {
            _menuGroupRepository = menuGroupRepository;
            _menuItemRepository = menuItemRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        

        public async Task<BaseResponse<PriceListEntry>> EditPriceListItemAsync(string userToken, Guid menuItemId, EditPriceListItemRequestModel request)
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
                    return new BaseResponse<PriceListEntry>
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
                return new BaseResponse<PriceListEntry>
                {
                    Message = $"MenuItem accessed doesnt exist..",
                    Status = false,

                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<PriceListEntry>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            existingMenuItem.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingMenuItem.LastModifiedOn = DateTime.UtcNow;
            existingMenuItem.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";
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
                existingMenuItem.MiniPriceRange = 0;
                existingMenuItem.MaxPriceRange = 0;
                existingMenuItem.RangePrices.Clear();

                foreach (var rangePrice in request.RangePrices)
                {
                    var existingRangePrice = existingMenuItem.RangePrices.FirstOrDefault(rp => rp.Name == rangePrice.Name);

                    if (existingRangePrice != null)
                    {
                        // Update the existing range price
                        existingRangePrice.Price = rangePrice.Price;
                    }
                    else
                    {
                        // Create a new range price if it doesn't exist
                        var price = new RangePriceOption
                        {
                            MenuItemId = existingMenuItem.Id,
                            Name = rangePrice.Name,
                            Price = rangePrice.Price
                        };

                        existingMenuItem.RangePrices.Add(price);
                    }

                    // Calculate min and max price here
                    var minPrice = existingMenuItem.RangePrices.Min(rp => rp.Price);
                    var maxPrice = existingMenuItem.RangePrices.Max(rp => rp.Price);

                    existingMenuItem.MiniPriceRange = minPrice;
                    existingMenuItem.MaxPriceRange = maxPrice;
                }
            }
            else if (existingMenuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.TimeSpecificPriceOption)
            {
                existingMenuItem.TimeSpecificPrice.Clear();

                foreach (var timeSpecificPrice in request.TimeSpecificPrice)
                {

                    var newTimeSpecificPrice = new TimeSpecificPriceOption
                    {
                        MenuItemId = existingMenuItem.Id,
                        BasePrice = timeSpecificPrice.BasePrice,
                        DiscountedPrice = timeSpecificPrice.DiscountedPrice,
                        StartDateTime = timeSpecificPrice.StartDateTime,
                        EndDateTime = timeSpecificPrice.EndDateTime
                    };

                    // Add the new time-specific price to the menu item
                    existingMenuItem.TimeSpecificPrice.Add(newTimeSpecificPrice);

                    // Check if the new time-specific price is currently active
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

            int maxRetries = 8;
            int retryCount = 0;
            bool success = false;

            while (!success && retryCount < maxRetries)
            {
                try
                {
                    // Attempt to update the menu item
                    var editedMenuItem = await _menuItemRepository.EditMenuItemAsync(existingMenuItem);

                    // If the update is successful, set success to true
                    success = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Handle the concurrency exception, log it if necessary

                    // Increment the retry count
                    retryCount++;
                }
            }

            if (!success)
            {
                // Handle the case where maximum retries were reached
                _logger.LogError("Maximum retries reached for concurrency handling.");
                return new BaseResponse<PriceListEntry>
                {
                    Message = "Concurrency conflict could not be resolved.",
                    Status = false
                };
            }

            _logger.LogError("Price update succeeded");
            return new BaseResponse<PriceListEntry>
            {
                Message = "Price update succeeded",
                Status = true
            };

        }

        //public async Task<BaseResponse<IList<PriceListEntry>>> GetPriceListAsync(string businessName)
        //{
        //    var menuGroups = await _menuGroupRepository.GetAllMenuGroupsByCompanyNameAsync(businessName);
        //    if(menuGroups is null || !menuGroups.Any())
        //    {
        //        _logger.LogError("MenuGroups are empty, couldn't be fetched");
        //        return new BaseResponse<IList<PriceListEntry>>
        //        {
        //            Message = $"MenuGroups are empty.",
        //            Status = false,

        //        };
        //    }
        //    var menuItems = await _menuItemRepository.GetAllMenuItemsByCompanyNameAsync(businessName);
        //    if (menuItems is null || !menuItems.Any())
        //    {
        //        _logger.LogError("MenuItems are empty, couldn't be fetched");
        //        return new BaseResponse<IList<PriceListEntry>>
        //        {
        //            Message = $"MenuItems are empty.",
        //            Status = false,

        //        };
        //    }

        //    var priceList = new List<PriceListEntry>();
        //    foreach (var group in menuGroups)
        //    {
        //        var priceListEntry = new PriceListEntry
        //        {
        //            MenuGroups = new List<MenuGroup> { group },
        //            Prices = new List<PriceOption>()
        //        };

        //        foreach (var menuItem in menuItems.Where(item => item.MenuGroupId == group.Id))
        //        {
        //            var priceOption = new PriceOption
        //            {
        //                OptionPrice = menuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.BasePrice
        //                    ? menuItem.CurrentPrice
        //                    : menuItem.MenuItemPricingOption == Domain.Domain.Modules.Users.Entities.Enums.MenuItemPricingOption.TimeSpecificPriceOption
        //                        ? menuItem.TimeSpecificPrice.FirstOrDefault()?.DiscountedPrice ?? menuItem.CurrentPrice
        //                        : 0, // Set to 0 for non-RangePrice options
        //                MinPrice = (decimal)(menuItem.MenuItemPricingOption == MenuItemPricingOption.RangePriceOption
        //                    ? menuItem.MiniPriceRange 
        //                    : 0), // Set to 0 for non-RangePrice options
        //                MaxPrice = (decimal)(menuItem.MenuItemPricingOption == MenuItemPricingOption.RangePriceOption
        //                    ? menuItem.MaxPriceRange
        //                    : 0) // Set to 0 for non-RangePrice options
        //            };

        //            priceListEntry.Prices.Add(priceOption);
        //        }

        //        priceList.Add(priceListEntry);
        //    }

        //    if(priceList is null || !priceList.Any())
        //    {
        //        _logger.LogError("Price list is empty");
        //        return new BaseResponse<IList<PriceListEntry>>
        //        {
        //            Message = "Price list is empty",
        //            Status = true
        //        };
        //    }

        //    _logger.LogError("Price list fetched");
        //    return new BaseResponse<IList<PriceListEntry>>
        //    {
        //        Message  = "Price list fetched",
        //        Data = priceList,
        //        Status = true
        //    };


        //}

        public async Task<BaseResponse<IList<PriceListResponseDto>>> GetPriceListAsync(string businessName)
        {
            var menuGroups = await _menuGroupRepository.GetAllMenuGroupsByCompanyNameAsync(businessName);
            if (menuGroups is null || !menuGroups.Any())
            {
                _logger.LogError("MenuGroups are empty, couldn't be fetched");
                return new BaseResponse<IList<PriceListResponseDto>>
                {
                    Message = "MenuGroups are empty.",
                    Status = false,
                };
            }

            var menuItems = await _menuItemRepository.GetAllMenuItemsByCompanyNameAsync(businessName);
            if (menuItems is null || !menuItems.Any())
            {
                _logger.LogError("MenuItems are empty, couldn't be fetched");
                return new BaseResponse<IList<PriceListResponseDto>>
                {
                    Message = "MenuItems are empty.",
                    Status = false,
                };
            }

            // Create a list to store the results
            var priceListDto = new List<PriceListResponseDto>();

            foreach (var group in menuGroups)
            {
                foreach (var menuItem in menuItems.Where(item => item.MenuGroupId == group.Id))
                {
                    // Construct the PriceListResponseDto for each menu item
                    var priceListEntry = new PriceListResponseDto
                    {
                        MenuItemId = menuItem.Id,
                        MenuGroupId = group.Id,
                        MenuItemName = menuItem.MenuItemName,
                        MenuGroupName = group.MenuGroupName,
                        BasePrice = (decimal)(menuItem.MenuItemPricingOption == MenuItemPricingOption.BasePrice
                            ? menuItem.BasePrice : 0),
                        CurrentPrice = menuItem.MenuItemPricingOption == MenuItemPricingOption.TimeSpecificPriceOption
                            ? menuItem.TimeSpecificPrice.FirstOrDefault()?.DiscountedPrice ?? menuItem.CurrentPrice
                            : menuItem.CurrentPrice,
                        MinPrice = (decimal)(menuItem.MenuItemPricingOption == MenuItemPricingOption.RangePriceOption
                            ? menuItem.MiniPriceRange
                            : 0), // Set to 0 for non-RangePrice options
                        MaxPrice = (decimal)(menuItem.MenuItemPricingOption == MenuItemPricingOption.RangePriceOption
                            ? menuItem.MaxPriceRange
                            : 0) // Set to 0 for non-RangePrice options
                    };

                    priceListDto.Add(priceListEntry);
                }
            }

            if (!priceListDto.Any())
            {
                _logger.LogError("Price list is empty");
                return new BaseResponse<IList<PriceListResponseDto>>
                {
                    Message = "Price list is empty",
                    Status = true
                };
            }

            _logger.LogError("Price list fetched");
            return new BaseResponse<IList<PriceListResponseDto>>
            {
                Message = "Price list fetched",
                Data = priceListDto,
                Status = true
            };
        }

    }
}
