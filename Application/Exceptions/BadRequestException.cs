using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException()
        { }

        public BadRequestException(string message)
            : base(message)
        { }

        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
