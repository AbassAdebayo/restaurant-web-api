using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class ModifierItemDto
    {
        public Guid Id { get; set; }
        public Guid ModifierGroupId { get; set; }
        public string ModifierItemName { get; set; }
        public EntityStatus? Status { get; set; }
        public string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateModifierItemRequestModel
    {
        
        //public string ModifierItemName { get; set; }
        public List<CreateListModifierModel> ModifierItemNames { get; set; }


    }

    public class CreateListModifierModel
    {
        public string ModifierItemName { get; set; }
    }

    public class EditModifierItemRequestModel
    {
        public string ModifierItemName { get; set; }
        
    }
}
