using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Exceptions.Messaging
{
    public class MailSenderException : Exception
    {
        public string Message { get; set; }

        public MailSenderException(string message)
            : base(message) { }

        public MailSenderException(string message, Exception innerException)
            : base(message, innerException) { }
        

        
    }
}
