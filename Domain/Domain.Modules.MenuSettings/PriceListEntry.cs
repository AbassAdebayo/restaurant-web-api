using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.MenuSettings
{
    public class PriceListEntry
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public ICollection<MenuGroup> MenuGroups { get; set; } = new HashSet<MenuGroup>();
        public ICollection<MenuItem> MenuItems { get; set; } = new HashSet<MenuItem>();
        public ICollection<PriceOption> Prices { get; set; } = new HashSet<PriceOption>();
    }
}
