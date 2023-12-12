using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.MenuSettings
{
    public class PriceOption
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaxPrice { get; set; }
    }
}
