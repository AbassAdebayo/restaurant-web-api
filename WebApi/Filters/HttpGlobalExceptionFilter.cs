using Application.DTOs;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WebApi.ActionResults;
using WebApi.Helpers;

namespace WebApi.Filters
{
    public class HttpGlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult),
                context.Exception,
                context.Exception.Message);

            if (context.Exception.GetType() == typeof(NotFoundException))
            {
                _logger.LogWarning(context.Exception, HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, HttpStatusCode.NotFound));
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new NotFoundObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception.GetType() == typeof(BadRequestException))
            {
                _logger.LogWarning(context.Exception, HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, HttpStatusCode.BadRequest));
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new BadRequestObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception.GetType() == typeof(ConflictException))
            {
                _logger.LogWarning(context.Exception, HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, HttpStatusCode.Conflict));
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new ConflictObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            }
            else if (context.Exception.GetType() == typeof(BadGatewayException))
            {
                _logger.LogWarning(context.Exception, HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, HttpStatusCode.BadGateway));
                var response = new BaseResponse
                {
                    Message = context.Exception.Message
                };

                context.Result = new BadGatewayObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadGateway;
            }
            else
            {
                _logger.LogWarning(context.Exception, HttpHelper.RequestToLogStringWithHttpStatusCode(context.HttpContext.Request, HttpStatusCode.InternalServerError));
                var response = new BaseResponse
                {
                    Message = "An error occurred. Try it again."
                };

                if (_env.IsDevelopment())
                {
                    response.Message = context.Exception.StackTrace;
                }

                context.Result = new InternalServerErrorObjectResult(response);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}

