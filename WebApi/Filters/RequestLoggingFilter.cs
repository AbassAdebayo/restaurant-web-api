using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using WebApi.Helpers;

namespace WebApi.Filters
{
    public class RequestLoggingFilter : IAsyncResultFilter
    {
        private readonly ILogger<RequestLoggingFilter> _logger;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as IStatusCodeActionResult;
            var statusCode = HttpStatusCode.OK;

            if (result?.StatusCode != null)
            {
                statusCode = (HttpStatusCode)result.StatusCode;
            }
            _logger.LogInformation(HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, statusCode));
            await next();
        }
    }
}
