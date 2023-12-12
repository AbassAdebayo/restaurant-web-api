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
    public class ModifierGroupDto
    {
        public Guid Id { get; set; } 
        public string ModifierGroupName { get; set; }
        public decimal ModifierGroupPrice { get; set; }
        public string CompanyName { get; set; }
        public ICollection<ModifierItemDto> ModifierItems { get; set; } = new HashSet<ModifierItemDto>();
        public string? RuleDescription { get; set; }
        public EntityStatus? Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateModifierGroupRequestModel
    {
        //public Guid MenuId { get; set; }
        public string ModifierGroupName { get; set; }
        public decimal ModifierGroupPrice { get; set; }

    }

    public class EditModifierGroupRequestModel
    {
        public string ModifierGroupName { get; set; }
        public decimal ModifierGroupPrice { get; set; }
    }

    public class ApplyModifierRulesToModifierGroupRequestModel
    {
        public List<ModifierRules> SelectedRules { get; set; }
    }
}
