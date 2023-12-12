using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class MenuItemDto
    {
        public Guid MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public MenuItemPricingOption MenuItemPricingOption { get; set; }
        public string MenuItemCode { get; set; }
        public string MenuGroupCode { get; set; }
        public string MenuItemImage { get; set; }
        public Channels Channel { get; set; }
        public decimal BasePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal? MiniPriceRange { get; set; }
        public decimal? MaxPriceRange { get; set; }
        public string CompanyName { get; set; }
        public EntityStatus Status { get; set; }
        public Guid MenuGroupId { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public ICollection<RangePriceOptionDto> RangePrices { get; set; } = new HashSet<RangePriceOptionDto>();
        public ICollection<TimeSpecificPriceOptionDto> TimeSpecificPrice { get; set; } = new HashSet<TimeSpecificPriceOptionDto>();
        public ICollection<ModifierGroup> ModifierGroups { get; set; } = new HashSet<ModifierGroup>();
        public string TagName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateMenuItemRequestModel
    {
        public string MenuItemName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
        public MenuItemPricingOption MenuItemPricingOption { get; set; }
        public string MenuItemImage { get; set; }
        public decimal BasePrice { get; set; }
        public List<TimeSpecificPriceOptionCreateRequestModel> TimeSpecificPrice { get; set; }
        public List<RangePriceOptionCreateRequestModel>  RangePrices { get; set; }
    }

    public class EditMenuItemRequestModel
    {
        public string MenuItemName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
        public MenuItemPricingOption MenuItemPricingOption { get; set; }
        public string MenuItemCode { get; set; }
        public string MenuItemImage { get; set; }
        public decimal BasePrice { get; set; }
        public List<TimeSpecificPriceOptionCreateRequestModel> TimeSpecificPrice { get; set; }
        public List<RangePriceOptionCreateRequestModel> RangePrices { get; set; }

    }
}
