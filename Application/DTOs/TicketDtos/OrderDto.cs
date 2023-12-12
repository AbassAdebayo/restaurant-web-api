using Domain.Domain.Modules.Order;
using Domain.Domain.Modules.Tables;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TicketDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid? TabId { get; set; }
        public Tab? Tab { get; set; }
        public string OrderNumber { get; set; } 
        public string WaiterName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerName { get; set; }
        public string CustomerReferenceNumber { get; set; }
        public OrderStatus Status { get; set; }
        public decimal? Bill { get; set; }
        public OrderChannel? Channel { get; set; }
        public decimal? Tip { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; } = new HashSet<OrderItemDto>();
    }

    public class CreateOrderRequestModel
    {    
        public string CustomerName { get; set; }
        public OrderChannel? Channel { get; set; }
        public decimal? Tip { get; set; }
        public List<CreateOrderItemRequestModel> OrderItems { get; set; }
    }
}
