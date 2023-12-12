using Domain.Domain.Modules.MenuSettings;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class PriceListEntryDto
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public ICollection<MenuGroup> MenuGroups { get; set; } = new HashSet<MenuGroup>();
        public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
        public ICollection<PriceOption> Prices { get; set; } = new HashSet<PriceOption>();
    }

    public class EditPriceListItemRequestModel
    {
        public decimal BasePrice { get; set; }
        //public decimal MinPriceRange { get; set; }
        //public decimal MaxPriceRange { get; set; }
        public List<TimeSpecificPriceOptionCreateRequestModel> TimeSpecificPrice { get; set; }
        public List<RangePriceOptionCreateRequestModel> RangePrices { get; set; }
    }

    public class PriceListResponseDto
    {
        public Guid MenuItemId { get; set; }
        public Guid MenuGroupId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuGroupName { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        //public decimal Price { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
