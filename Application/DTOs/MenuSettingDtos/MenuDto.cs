using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class MenuDto
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuCode { get; set; }
        public string CreatedBy { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string TagName { get; set; }
        public ICollection<MenuGroupDto> MenuGroups { get; set; } = new HashSet<MenuGroupDto>();
        public Channels Channel { get; set; }
        public EntityStatus Status { get; set; }
    }

    public class CreateMenuRequestModel
    {
        public string MenuName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
    }

    public class EditMenuRequestModel
    {
        public string MenuName { get; set; }
        public Channels Channel { get; set; }
        public string TagName { get; set; }
    }
}
