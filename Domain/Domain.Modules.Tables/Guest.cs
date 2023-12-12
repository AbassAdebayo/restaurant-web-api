using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Tables
{
    public class Guest : AuditableEntity
    {
        public int NumberOfGuest { get; set; }
        public string CompanyName { get; set; }
        public Guid TableId { get; set; }
        public Table? Table { get; set; }
        
    }
}
