using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TableDomainException : Exception
    {
        public TableDomainException()
        { }

        public TableDomainException(string message)
            : base(message)
        { }

        public TableDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
