using Domain.Contracts;
using Domain.Domain.Modules.Users.Entities;
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
    public class Menu : AuditableEntity
    {
        public string MenuName { get; set; }
        public string MenuCode { get; set; }

        [Column]
        public string CompanyName { get; set; }
        public ICollection<MenuGroup> MenuGroups { get; set; } = new HashSet<MenuGroup>();

        [Column]
        public string TagName { get; set; } 
        public Channels Channel { get; set; }
        public EntityStatus Status { get; set; }

        

    }
}
