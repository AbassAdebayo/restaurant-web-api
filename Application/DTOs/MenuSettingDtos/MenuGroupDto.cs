using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class MenuGroupDto
    {
        public Guid MenuGroupId { get; set; }
        public string MenuGroupName { get; set; }
        public string CompanyName { get; set; }

        // Yes or No
        public bool MenuGroupPricingOption { get; set; }
        public decimal MenuGroupPrice { get; set; }
        public string MenuGroupCode { get; set; }
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<MenuItemDto> MenuItems { get; set; } = new HashSet<MenuItemDto>();
        public string TagName { get; set; }
        public string MenuGroupImage { get; set; }
        public Channels Channel { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateMenuGroupRequestModel
    {
        //public Guid MenuId { get; set; }
        public string MenuGroupName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
        public bool MenuGroupPricingOption { get; set; }
        public decimal MenuGroupPrice { get; set; }
        public string MenuGroupImage { get; set; }
    }

    public class EditMenuGroupRequestModel
    {
        public string MenuGroupName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
        public bool MenuGroupPricingOption { get; set; }
        public decimal MenuGroupPrice { get; set; }
        public string MenuGroupImage { get; set; }
    }
}
