using Domain.Contracts;
using Domain.Domain.Modules.Tables;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Order
{
    public class Order : AuditableEntity
    {
        public Guid? TabId { get; set; }
        public Tab? Tab { get; set; }
        [Column]
        public string OrderNumber { get; set; }
        [Column]
        public string? WaiterName { get; set; }
        [Column]
        public string? CompanyName { get; set; }
        [Column]
        public string CustomerName { get; set; }
        [Column]
        public string CustomerReferenceNumber { get; set; }
        [Column]
        public OrderStatus Status { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Bill { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        public OrderChannel? Channel { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Tip { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();


        public void SetPaidStatus()
        {
            if(Status != OrderStatus.Served)
            {
                StatusChangeException(OrderStatus.Paid);
            }

            Status = OrderStatus.Paid;  
        }

        public void SetDeliveredStatus()
        {
            if (Status != OrderStatus.Paid)
            {
                StatusChangeException(OrderStatus.Completed);
            }

            Status = OrderStatus.Completed;
            
        }

        public void SetCancelledStatus()
        {
            if (Status == OrderStatus.Paid || Status == OrderStatus.Completed)
            {
                StatusChangeException(OrderStatus.Cancelled);
            }

            Status = OrderStatus.Cancelled;
           
        }

        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new OrderingDomainException($"It's not possible to change the order status from {Status.ToString()} to {orderStatusToChange.ToString()}.");
        }

        public decimal GetOrderItemsTotalPrice()
        {
            return OrderItems.Sum(o => o.Quantity * o.UnitPrice);
        }

    }
}
