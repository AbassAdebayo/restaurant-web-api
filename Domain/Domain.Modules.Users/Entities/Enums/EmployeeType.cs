using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.Users.Entities.Enums
{
    public enum EmployeeType
    {
        [Description("FullTime")]
        FullTime = 1,

        [Description("PartTime")]
        PartTime,

        [Description("Contract")]
        Contract
    }
}
