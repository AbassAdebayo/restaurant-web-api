using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class CustomException : Exception
    {
        public List<string> ErrorMessages { get; } = new();

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, List<string> errors = default, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}
