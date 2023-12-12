using Domain.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.MenuSettings
{
    public class TimeSpecificPriceOption
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public Guid MenuItemId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountedPrice { get; set; }
        [Column]
        public DateTime StartDateTime { get; set; }
        [Column]
        public DateTime EndDateTime { get; set; }
    }
}
