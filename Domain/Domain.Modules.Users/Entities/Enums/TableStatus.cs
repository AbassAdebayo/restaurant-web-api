using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{
    public enum TableStatus
    {
        [Description("Opened")]
        Opened = 1,

        [Description("Closed")]
        Closed,

    }
}
