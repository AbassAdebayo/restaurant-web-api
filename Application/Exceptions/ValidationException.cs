using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]>? ErrorDictrionary { get; }

        public HttpStatusCode StatusCode { get; }

        public ValidationException(Dictionary<string, string[]>? errors = default, HttpStatusCode statusCode = HttpStatusCode.BadGateway)
            : base()
        {
            ErrorDictrionary = errors;
            StatusCode = statusCode;
        }
    }
}
