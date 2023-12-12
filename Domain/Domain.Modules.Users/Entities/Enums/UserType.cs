using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Enums
{
    public enum UserType
    {
        [Description("SuperAdmin")]
        SuperAdmin = 1,

        [Description("Employee")]
        Employee
    }
}
