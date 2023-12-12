using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public string MenuItemName { get; set; }
        public decimal UnitPrice { get; set; }
        //public ICollection<OrderItemPrice> UnitPrices { get; set; } = new HashSet<OrderItemPrice>();
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateOrderItemRequestModel
    {
        public Guid MenuItemId { get; set; }
        public string? RangeName { get; set; }
        //public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
