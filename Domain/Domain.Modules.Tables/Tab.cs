using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Tables
{
    public class Tab : AuditableEntity
    {
        public string TabName { get; set; }
        public string CompanyName { get; set; }
        public Guid TableId { get; set; }
        public Table? Table { get; set; }
        public ICollection<Domain.Modules.Order.Order> Orders { get; set; } = new HashSet<Domain.Modules.Order.Order>();
    }
}
