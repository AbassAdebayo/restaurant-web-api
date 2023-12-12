using Domain.Contracts;
using Domain.Domain.Modules.Order;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.MenuSettings
{
    public class MenuItem : AuditableEntity
    {
        [Column]
        public string MenuItemName { get; set; }
        public MenuItemPricingOption MenuItemPricingOption { get; set; }
        [Column]
        public string MenuItemCode { get; set; }
        [Column]
        public string MenuGroupCode { get; set; }
        [Column]
        public string MenuItemImage { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal BasePrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CurrentPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MiniPriceRange { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MaxPriceRange { get; set; }

        [Column]
        public string CompanyName { get; set; }
        [Column]
        public Channels Channel { get; set; }
        public EntityStatus Status { get; set; }
        public Guid MenuGroupId { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public ICollection<RangePriceOption> RangePrices { get; set; } = new HashSet<RangePriceOption>();
        public ICollection<TimeSpecificPriceOption> TimeSpecificPrice { get; set; } = new HashSet<TimeSpecificPriceOption>();
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        [Column]
        public string TagName { get; set; }

    }
}
