using Domain.Contracts;
using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Order;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Tables
{
    public class Table : AuditableEntity
    {
        [Column]
        public string TableNumber { get; set; }
        public string TableId { get; set; }
        [Column]
        public string QrCode { get; set; }
        public bool? IsActive { get; set; }
        public string? BranchName { get; set; }
        public int? TableCapacity { get; set; }
        [Column]
        public TableStatus? Status { get; set; } 
        [Column]
        public string CompanyName { get; set; }
        public Guid? MenuId { get; set; } 
        public Menu? Menu { get; set; }
        public ICollection<Guest> Guests { get; set; } = new HashSet<Guest>();
        public ICollection<Tab> Tabs { get; set; } = new HashSet<Tab>();

        public void SetTableToOpenedStatus()
        { 
            Status = TableStatus.Opened;
        }

        public void SetTableToClosedStatus()
        {
            if (Status != TableStatus.Opened)
            {
                StatusChangeException(TableStatus.Closed);
            }

            Status = TableStatus.Closed;

        }

        public void ConfigureTableCapacity(int tableCapacity)
        {
            if(tableCapacity <= 0)
            {
                throw new TableDomainException(nameof(tableCapacity));
            }

            TableCapacity = tableCapacity;
        }

        public void SetTableToActiveOrInActiveStatus(bool isActive)
        {

            if (isActive)
            {
                IsActive = true;
            }

            IsActive = false;

        }
        private void StatusChangeException(TableStatus orderStatusToChange)
        {
            throw new OrderingDomainException($"It's not possible to change the order status from {Status.ToString()} to {orderStatusToChange.ToString()}.");
        }

    }
}
