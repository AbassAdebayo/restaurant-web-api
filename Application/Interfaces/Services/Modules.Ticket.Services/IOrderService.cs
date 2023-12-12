using Application.DTOs;
using Application.DTOs.TicketDtos;
using Domain.Domain.Modules.Order;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Modules.Ticket.Services
{
    public interface IOrderService
    {
        Task<BaseResponse<Order>> AddOrderAsync(CreateOrderRequestModel request, Guid tabId, string userToken);

        Task<Order> Update(Order order);

        Task<BaseResponse<OrderDto>> GetOrderByIdAsync(Guid orderId);
        Task<BaseResponse<OrderDto>> GetOrderByOrderNumberAsync(string orderNumber);
        Task<BaseResponse<long>> GetCountOfAllOrdersByBusinessNameAsync(string businessName);
        Task<BaseResponse<decimal>> GetTotalPriceForAllPaidOrdersByBusinessNameAsync(string businessName);
        Task<PaginatedResult<Order>> GetAllOrdersPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter);
        Task<PaginatedResult<Order>> GetOrdersFromCustomerPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter, string customerName);
        Task<IList<Order>> GetOrdersFromCustomerReferenceNumberByBusinessNameAsync(string businessName, string customerReferenceNumber);
        Task<BaseResponse<bool>> CancelOder(Guid orderId);
    }
}
