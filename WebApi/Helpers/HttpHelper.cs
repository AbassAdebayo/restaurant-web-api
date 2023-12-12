using Microsoft.AspNetCore.Http.Extensions;
using NLog;
using System.Net;
using System.Text;

namespace WebApi.Helpers
{
    public static class HttpHelper
    {
        public static string RequestToLogString(HttpRequest request)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{request.Method} {request.GetDisplayUrl()}");
            stringBuilder.AppendLine($"Headers {HeadersToString(request.Headers)}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(MappedDiagnosticsLogicalContext.Get("HttpData"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        public static string RequestToLogStringWithHttpStatusCode(HttpRequest request, HttpStatusCode statusCode)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{request.Method} {request.GetDisplayUrl()}");
            stringBuilder.AppendLine($"Response {(int)statusCode}");
            stringBuilder.AppendLine($"Headers {HeadersToString(request.Headers)}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine(MappedDiagnosticsLogicalContext.Get("HttpData"));
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            return stringBuilder.ToString();
        }

        private static string HeadersToString(IHeaderDictionary headers)
        {
            var stringBuilder = new StringBuilder();

            foreach (var header in headers)
            {
                stringBuilder.AppendLine($"{header.Key}: {header.Value}");
            }

            return stringBuilder.ToString();
        }
    }
}
