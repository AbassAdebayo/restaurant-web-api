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
    public class RangePriceOption 
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public Guid MenuItemId { get; set; }
        [Column]
        public string Name { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
       
    }
}
