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
    public class ModifierItem : AuditableEntity
    {
        public Guid ModifierGroupId { get; set; }
        [Column]
        public string ModifierItemName { get; set; }
        [Column]
        public string CompanyName { get; set; }
        public EntityStatus? Status { get; set; }

    }
}
