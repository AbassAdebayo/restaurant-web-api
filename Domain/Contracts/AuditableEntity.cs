using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity, ISoftDelete
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedOn { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
