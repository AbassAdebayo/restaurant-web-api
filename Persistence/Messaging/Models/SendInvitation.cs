using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Messaging.Models
{
    public class SendInvitation : Base
    {
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; }
    }
}
