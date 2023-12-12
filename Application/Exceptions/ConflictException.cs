using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException()
        { }

        public ConflictException(string message)
            : base(message)
        { }

        public ConflictException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
