using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.ActionResults
{
    public class BadGatewayObjectResult : ObjectResult
    {
        public BadGatewayObjectResult(object error)
           : base(error)
        {
            StatusCode = StatusCodes.Status502BadGateway;
        }
    }
}
