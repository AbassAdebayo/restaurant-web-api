using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Messaging.Models
{
    public class ForgotPassword : Base
    {
        public string PasswordResetLink { get; set; }
    }
}
