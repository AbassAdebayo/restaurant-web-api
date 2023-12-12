using Domain.Contracts;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.MenuSettings
{
    public class ModifierGroup : AuditableEntity
    {
        [Column]
        public string ModifierGroupName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ModifierGroupPrice { get; set; }
        [Column]
        public string CompanyName { get; set; }
        [Column]
        public string? RuleDescription { get; set; }
        public EntityStatus? Status { get; set; }
        public ICollection<ModifierItem> ModifierItems { get; set; } = new HashSet<ModifierItem>();
       

    }
}
