using Application.DTOs;
using Application.DTOs.TicketDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Repositories;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces;
using Application.Interfaces.Services.Modules.Ticket.Services;
using Domain.Domain.Modules.Order;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.Modules.Ticket.Repositories;
using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Users.Entities.Enums;
using Application.Exceptions;

namespace Application.Implementations.Modules.Ticket.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ITabRepository _tabRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository, ITabRepository tabRepository, ITableRepository tableRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
            _tabRepository = tabRepository;
            _tableRepository = tableRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<Order>> AddOrderAsync(CreateOrderRequestModel request, Guid tabId, string userToken)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<Order>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            // check if user is confirmed
            //if (!user.EmailConfirmed) throw new BadRequestException("User unverified");

            if (user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Menu Settings");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<Order>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            var getTab = await _tabRepository.GetTabByIdAsync(tabId);
            if (getTab == null)
            {
                _logger.LogError("The Table accessed couldn't be fetched!");
                return new BaseResponse<Order>
                {
                    Message = "The Table accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Order>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            // Generate OrderNumber
            string orderNumber = GenerateOrderNumber();

            // Generate Customer Reference Number
            string customerReferenceNumber = GenerateCustomerReferenceNumber(request.CustomerName, request.OrderItems.Count);

            // Process order items to get MenuItemName and calculate unit price
            var processedOrderItems = new List<OrderItem>();
            foreach (var itemModel in request.OrderItems)
            {
                var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(itemModel.MenuItemId);
                if (menuItem == null)
                {
                    _logger.LogError("The MenuItem accessed couldn't be fetched!");
                    return new BaseResponse<Order>
                    {
                        Message = "The MenuItem accessed couldn't be fetched!",
                        Status = false
                    };
                }

                decimal unitPrice;

                switch (menuItem.MenuItemPricingOption)
                {
                    case MenuItemPricingOption.BasePrice:
                        unitPrice = menuItem.CurrentPrice;
                        break;

                    case MenuItemPricingOption.TimeSpecificPriceOption:
                        unitPrice = menuItem.CurrentPrice;
                        break;

                    case MenuItemPricingOption.RangePriceOption:
                        // Add all range prices to the unitPrices collection
                        var selectedRangePrice = menuItem.RangePrices.FirstOrDefault(rp => rp.Name == itemModel.RangeName);
                        unitPrice = selectedRangePrice.Price;
                        break;

                    // Add other pricing options as needed

                    default:
                        _logger.LogError("Pricing Option not found!");
                        return new BaseResponse<Order>
                        {
                            Message = "Pricing Option not found!",
                            Status = false
                        };
                }

                var processedItem = new OrderItem
                {
                    MenuItemId = itemModel.MenuItemId,
                    MenuItemName = menuItem.MenuItemName,
                    UnitPrice = unitPrice,
                    Quantity = itemModel.Quantity
                };

                processedOrderItems.Add(processedItem);
            }

            Order order = new Order
            {
                TabId = tabId,
                OrderNumber = orderNumber,
                WaiterName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? null : $"{user.Name} {user.LastName}",
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CustomerName = request.CustomerName,
                CustomerReferenceNumber = customerReferenceNumber,
                Status = OrderStatus.Ordered,
                OrderItems = processedOrderItems,
                Channel = request.Channel,
                Tip = request.Tip
            };
            order.Bill = order.GetOrderItemsTotalPrice();
            var createNewOrder = await _orderRepository.AddOrderAsync(order);
            if (createNewOrder is null)
            {
                _logger.LogError("Order instantiation couldn't be completed.");
                return new BaseResponse<Order>
                {
                    Message = "Order instantiation couldn't be completed.",
                    Status = false
                };
            }


            var getTable = await _tableRepository.GetTableByTabAsync(getTab);

            getTable.SetTableToOpenedStatus();
            var updateTable = await _tableRepository.EditTableAsync(getTable);

            if(updateTable is null)
            {
                _logger.LogError("Order instantiated but unable to set table status.");
                return new BaseResponse<Order>
                {
                    Message = "Order instantiated but unable to set table status.",
                    Status = false
                };
            }

            _logger.LogError("Order has been instantiated successfully.");
            return new BaseResponse<Order>
            {
                Message = "Order has been instantiated successfully.",
                Status = true,
                Data = createNewOrder
            };
        }

        public async Task<BaseResponse<bool>> CancelOder(Guid orderId)
        {
            var getOrder = await _orderRepository.GetOrderByIdAsync(orderId);
            if(getOrder is null)
            {
                _logger.LogError("Order doesn't exist.");
                return new BaseResponse<bool>
                {
                    Message = "Order doesn't exist.",
                    Status = false
                };
            }

            getOrder.SetCancelledStatus();
            var orderToUpdate = await _orderRepository.Update(getOrder);

            if (orderToUpdate is null)
            {
                _logger.LogError("Order unable to cancel.");
                return new BaseResponse<bool>
                {
                    Message = "Order unable to cancel.",
                    Status = false
                };
            }

            _logger.LogError("Order cancelled successfully.");
            return new BaseResponse<bool>
            {
                Message = "Order cancelled successfully.",
            };
        }

        public Task<PaginatedResult<Order>> GetAllOrdersPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<long>> GetCountOfAllOrdersByBusinessNameAsync(string businessName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<OrderDto>> GetOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<OrderDto>> GetOrderByOrderNumberAsync(string orderNumber)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<Order>> GetOrdersFromCustomerPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter, string customerName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Order>> GetOrdersFromCustomerReferenceNumberByBusinessNameAsync(string businessName, string customerReferenceNumber)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<decimal>> GetTotalPriceForAllPaidOrdersByBusinessNameAsync(string businessName)
        {
            throw new NotImplementedException();
        }

        public Task<Order> Update(Order order)
        {
            throw new NotImplementedException();
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid()}";
        }

        private string GenerateCustomerReferenceNumber(string customerName, int orderItemCount)
        {
            return $"{DateTime.UtcNow:yyyyMMddHHmmss}-{customerName.Substring(0, 3)}-{orderItemCount}";
        }
    }
}
