using Domain.Contracts;
using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Order
{
    public class OrderItem : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public string MenuItemName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }
        //public ICollection<OrderItemPrice> UnitPrices { get; set; } = new HashSet<OrderItemPrice>();
        public int Quantity { get; set; }
    }
}
