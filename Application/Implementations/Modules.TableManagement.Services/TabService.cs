using Application.DTOs.TableManagementDtos;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Domain.Domain.Modules.Tables;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Application.DTOs.MenuSettingDtos;
using Application.Exceptions;

namespace Application.Implementations.Modules.TableManagement.Services
{
    public class TabService : ITabService
    {
        private readonly ITabRepository _tabRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<TabService> _logger;

        public TabService(ITabRepository tabRepository, ITableRepository tableRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<TabService> logger)
        {
            _tabRepository = tabRepository;
            _tableRepository = tableRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<Tab>> AddTabAsync(string userToken, Guid tableId, AddTabRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);

            // get email from claims
            var email = claims.SingleOrDefault(c => c.Type == "email");

            // get user by email
            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<Tab>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            // check if user is verified
            //if (!user.EmailConfirmed) throw new BadRequestException("User unverified");

            if (user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Table Ordering");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<Tab>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            // get selected table by Id
            var getTableNumber = await _tableRepository.GetTableByIdAsync(tableId);

            if (getTableNumber is null)
            {
                _logger.LogError("The Table number accessed couldn't be fetched!");
                return new BaseResponse<Tab>
                {
                    Message = "The Table number accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Tab>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }
            
            var newTab = new Tab
            {
                TabName = request.TabName,
                TableId = getTableNumber.Id,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CreatedOn = DateTime.UtcNow
            };

            var createNewTab = await _tabRepository.AddTabAsync(newTab);
            if (createNewTab is null)
            {
                _logger.LogError("Tab couldn't be created.");
                return new BaseResponse<Tab>
                {
                    Message = "Tab couldn't be created.",
                    Status = false
                };
            }

            _logger.LogError("Tab created successfully.");
            return new BaseResponse<Tab>
            {
                Message = "Tab created successfully.",
                Status = true,
                Data = createNewTab
            };
        }

        public async Task<BaseResponse<IList<TabDto>>> GetAllTabsByCompanyAsync(string companyName)
        {
            // get list of tabs by business
            var tabs = await _tabRepository.GetAllTabsByCompanyAsync(companyName);

            if (tabs is null || !tabs.Any())
            {
                _logger.LogError("Tabs are empty, couldn't be fetched");
                return new BaseResponse<IList<TabDto>>
                {
                    Message = $"Tabs are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            // map tabs
            var tabsDto = tabs.Select(t => new TabDto
            {
                TabId = t.Id,
                TabName = t.TabName,
                TableId = t.TableId,
                CompanyName = t.CompanyName,
                CreatedBy = t.CreatedBy,
                CreatedOn = t.CreatedOn,
                Orders = t.Orders.Select(o => new DTOs.TicketDtos.OrderDto
                {
                    Id = o.Id,
                    Bill = o.Bill,
                    Channel = o.Channel,
                    CustomerName = o.CustomerName,
                    CustomerReferenceNumber = o.CustomerReferenceNumber,
                    OrderNumber = o.OrderNumber,
                    Tip = o.Tip,
                    WaiterName = o.WaiterName,
                    TabId = o.TabId,
                    Status = o.Status,
                    CreatedBy = o.CreatedBy,
                    CreatedOn = o.CreatedOn,
                    OrderItems = o.OrderItems.Select(or => new DTOs.TicketDtos.OrderItemDto
                    {
                        Id = or.Id,
                        MenuItemId = or.MenuItemId,
                        MenuItemName = or.MenuItemName,
                        OrderId = or.OrderId,
                        UnitPrice = or.UnitPrice,
                        Quantity = or.Quantity,
                        CreatedBy = or.CreatedBy,
                        CreatedOn = or.CreatedOn,
                        ModifiedOn = or.LastModifiedOn


                    }).ToList(),

                }).ToList()

            }).ToList();

            return new BaseResponse<IList<TabDto>>
            {
                Message = "Tabs fetched successfully",
                Status = true,
                Data = tabsDto
            };
        }

        public async Task<BaseResponse<TabDto>> GetTabByIdAsync(Guid tabId)
        {
            var tab = await _tabRepository.GetTabByIdAsync(tabId);

            if (tab is null)
            {
                _logger.LogError("Tab couldn't be fetched");
                return new BaseResponse<TabDto>
                {
                    Message = $"Tab couldn't be fetched",
                    Status = false,

                };
            }

            var tabDto = new TabDto
            {
                TabId = tab.Id,
                TabName = tab.TabName,
                TableId = tab.TableId,
                CompanyName = tab.CompanyName,
                CreatedBy = tab.CreatedBy,
                CreatedOn = tab.CreatedOn,
                Orders = tab.Orders.Select(o => new DTOs.TicketDtos.OrderDto
                {
                    Id = o.Id,
                    Bill = o.Bill,
                    Channel = o.Channel,
                    CustomerName = o.CustomerName,
                    CustomerReferenceNumber = o.CustomerReferenceNumber,
                    OrderNumber = o.OrderNumber,
                    Tip = o.Tip,
                    WaiterName = o.WaiterName,
                    TabId = o.TabId,
                    Status = o.Status,
                    CreatedBy = o.CreatedBy,
                    CreatedOn = o.CreatedOn,
                    OrderItems = o.OrderItems.Select(or => new DTOs.TicketDtos.OrderItemDto
                    {
                        Id = or.Id,
                        MenuItemId = or.MenuItemId,
                        MenuItemName = or.MenuItemName,
                        OrderId = or.OrderId,
                        UnitPrice = or.UnitPrice,
                        Quantity = or.Quantity,
                        CreatedBy = or.CreatedBy,
                        CreatedOn = or.CreatedOn,
                        ModifiedOn = or.LastModifiedOn


                    }).ToList(),

                }).ToList()


            };

            _logger.LogError("Tab fetched successfully");
            return new BaseResponse<TabDto>
            {
                Message = "Tab fetched successfully",
                Status = true,
                Data = tabDto
            };
        }

        //public async Task<BaseResponse<TabDto>> GetTabByNameAsync(string tabName)
        //{
        //    var tab = await _tabRepository.GetTabByNameAsync(tabName);
        //    if (tab is null)
        //    {
        //        _logger.LogError("Tab couldn't be fetched");
        //        return new BaseResponse<TabDto>
        //        {
        //            Message = $"Tab couldn't be fetched",
        //            Status = false,

        //        };
        //    }

        //    var tabDto = new TabDto
        //    {
        //        TabId = tab.Id,
        //        TabName = tab.TabName,
        //        TableId = tab.TableId,
        //        CompanyName = tab.CompanyName,
        //        CreatedBy = tab.CreatedBy,
        //        CreatedOn = tab.CreatedOn,
        //        Orders = tab.Orders.Select(o => new DTOs.TicketDtos.OrderDto
        //        {
        //            Id = o.Id,
        //            Bill = o.Bill,
        //            Channel = o.Channel,
        //            CustomerName = o.CustomerName,
        //            CustomerReferenceNumber = o.CustomerReferenceNumber,
        //            OrderNumber = o.OrderNumber,
        //            Tip = o.Tip,
        //            WaiterName = o.WaiterName,
        //            TabId = o.TabId,
        //            Status = o.Status,
        //            CreatedBy = o.CreatedBy,
        //            CreatedOn = o.CreatedOn,
        //            OrderItems = o.OrderItems.Select(or => new DTOs.TicketDtos.OrderItemDto
        //            {
        //                Id = or.Id,
        //                MenuItemId = or.MenuItemId,
        //                MenuItemName = or.MenuItemName,
        //                OrderId = or.OrderId,
        //                UnitPrice = or.UnitPrice,
        //                Quantity = or.Quantity,
        //                CreatedBy = or.CreatedBy,
        //                CreatedOn = or.CreatedOn,
        //                ModifiedOn = or.LastModifiedOn


        //            }).ToList(),

        //        }).ToList()


        //    };

        //    _logger.LogError("Tab fetched successfully");
        //    return new BaseResponse<TabDto>
        //    {
        //        Message = "Tab fetched successfully",
        //        Status = true,
        //        Data = tabDto
        //    };
        //}

        public async Task<BaseResponse<IList<TabDto>>> GetTabsForTableAsync(Guid tableId)
        {
            var tabs = await _tabRepository.GetTabsForTableAsync(tableId);
            if (tabs is null || !tabs.Any())
            {
                _logger.LogError("Tabs are empty, couldn't be fetched");
                return new BaseResponse<IList<TabDto>>
                {
                    Message = $"Tabs are empty, or couldn't be fetched",
                    Status = false,

                };
            }

            var tabsDto = tabs.Select(t => new TabDto
            {
                TabId = t.Id,
                TabName = t.TabName,
                TableId = t.TableId,
                CompanyName = t.CompanyName,
                CreatedBy = t.CreatedBy,
                CreatedOn = t.CreatedOn,
                Orders = t.Orders.Select(o => new DTOs.TicketDtos.OrderDto
                {
                    Id = o.Id,
                    Bill = o.Bill,
                    Channel = o.Channel,
                    CustomerName = o.CustomerName,
                    CustomerReferenceNumber = o.CustomerReferenceNumber,
                    OrderNumber = o.OrderNumber,
                    Tip = o.Tip,
                    WaiterName = o.WaiterName,
                    TabId = o.TabId,
                    Status = o.Status,
                    CreatedBy = o.CreatedBy,
                    CreatedOn = o.CreatedOn,
                    OrderItems = o.OrderItems.Select(or => new DTOs.TicketDtos.OrderItemDto
                    {
                        Id = or.Id,
                        MenuItemId = or.MenuItemId,
                        MenuItemName = or.MenuItemName,
                        OrderId = or.OrderId,
                        UnitPrice = or.UnitPrice,
                        Quantity = or.Quantity,
                        CreatedBy = or.CreatedBy,
                        CreatedOn = or.CreatedOn,
                        ModifiedOn = or.LastModifiedOn


                    }).ToList(),

                }).ToList()

            }).ToList();

            return new BaseResponse<IList<TabDto>>
            {
                Message = "Tabs fetched successfully",
                Status = true,
                Data = tabsDto
            };
        }
    }
}
