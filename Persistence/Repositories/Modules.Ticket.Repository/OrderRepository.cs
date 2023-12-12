using Application.Interfaces.Repositories.Modules.Ticket.Repositories;
using Domain.Domain.Modules.Order;
using Domain.Wrapper;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Ticket.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<IList<Order>> GetAllOrdersByBusinessNameAsync(string businessName)
        {
            return await _context.Orders
                .Include(or => or.OrderItems)
                .Where(b => b.CompanyName == businessName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginatedResult<Order>> GetAllOrdersPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.CompanyName == businessName)
                .AsNoTracking().
                ToPaginatedResultListAsync(filter.PageNumber, filter.PageSize);

            return orders;

        }

        public async Task<long> GetCountOfAllOrdersByBusinessNameAsync(string businessName)
        {
            return await _context.Orders
                .Where(o => o.CompanyName == businessName)
                .CountAsync();
        }


        public async Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _context
                            .Orders
                            .Include(x => x.OrderItems)
                            .FirstOrDefaultAsync(o => o.Id == orderId);

            return order;
        }

        public async Task<Order> GetOrderByOrderNumberAsync(string orderNumber)
        {
            var order = await _context
                            .Orders
                            .Include(x => x.OrderItems)
                            .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);

            return order;
        }

        public async Task<IList<Order>> GetOrdersFromCustomerByBusinessNameAsync(string businessName, string customerName)
        {
            return await _context.Orders
                .Include(or => or.OrderItems)
                .Where(b => b.CompanyName == businessName && b.CompanyName == customerName)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PaginatedResult<Order>> GetOrdersFromCustomerPaginatedByBusinessNameAsync(string businessName, PaginationFilter filter, string customerName)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.CompanyName == businessName && o.CustomerName == customerName)
                .AsNoTracking().
                ToPaginatedResultListAsync(filter.PageNumber, filter.PageSize);

            return orders;
        }

        public async Task<IList<Order>> GetOrdersFromCustomerReferenceNumberByBusinessNameAsync(string businessName, string customerReferenceNumber)
        {
            return await _context.Orders
                .Include(or => or.OrderItems)
                .Where(b => b.CompanyName == businessName && b.CustomerReferenceNumber == customerReferenceNumber)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<decimal> GetTotalPriceForAllPaidOrdersByBusinessNameAsync(string businessName)
        {
            List<decimal?> prices = await _context.Orders
            .Where(a => a.CompanyName == businessName && a.Status == Domain.Domain.Modules.Users.Entities.Enums.OrderStatus.Paid)
            .Include(a => a.OrderItems)
            .Select(a => (decimal?)a.GetOrderItemsTotalPrice())
            .ToListAsync();

            return prices.Sum() ?? 0;
            

        }

        public async Task<Order> Update(Order order)
        {
             _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
