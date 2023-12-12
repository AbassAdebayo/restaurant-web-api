using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{
    public enum OrderStatus
    {
        Ordered = 1, // The client just send a new order
        Paid,
        Accepted,
        InProcess, // The order is approved by the waiter
        Cooking, // The cook accepted the order and started
        Cooked, // The cooking is done
        Served, // The waiter served everything
        Completed, // Everything is served and paid for
        Cancelled // The client/cook/waiter cancelled the order and it should not be cooked
    }
}
