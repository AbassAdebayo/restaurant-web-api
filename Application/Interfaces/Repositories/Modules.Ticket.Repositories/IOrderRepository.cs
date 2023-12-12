using Domain.Domain.Modules.Order;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Ticket.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);

        Task<Order> Update(Order order);

        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<Order> GetOrderByOrderNumberAsync(string orderNumber);
        Task<long> GetCountOfAllOrdersByBusinessNameAsync(string businessName);
        Task<decimal> GetTotalPriceForAllPaidOrdersByBusinessNameAsync(string businessName);
        Task<IList<Order>> GetAllOrdersByBusinessNameAsync(string businessName);
        Task<PaginatedResult<Order>> GetAllOrdersPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter);
        Task<IList<Order>> GetOrdersFromCustomerByBusinessNameAsync(string businessName, string customerName);
        Task<PaginatedResult<Order>> GetOrdersFromCustomerPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter, string customerName);
        Task<IList<Order>> GetOrdersFromCustomerReferenceNumberByBusinessNameAsync(string businessName, string customerReferenceNumber);
    }
}
