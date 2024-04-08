using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.PaymentGateway
{
    public class Payment: AuditableEntity
    {
        [Column]
        public string PaymentReferenceNumber { get; set; }
        [Column]
        public string? CustomerName { get; set; }
        [Column]
        public Guid OrderId { get; set; }
        [Column]
        public Domain.Modules.Order.Order Order { get; set; }
        
    }
}
