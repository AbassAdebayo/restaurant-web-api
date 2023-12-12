using Domain.Contracts;
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
    public class MenuGroup : AuditableEntity
    {
        public string MenuGroupName { get; set; }

        [Column]
        public string CompanyName { get; set; }

        // Yes or No
        public bool MenuGroupPricingOption { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal MenuGroupPrice { get; set; }
        public string MenuGroupCode { get; set; }
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();

        [Column]
        public string TagName { get; set; }
        public string MenuGroupImage { get; set; }
        public Channels Channel { get; set; }
        public EntityStatus Status { get; set; }


    }
}
