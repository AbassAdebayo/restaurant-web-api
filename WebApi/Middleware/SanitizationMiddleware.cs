

using Ganss.Xss;
using Humanizer;
using Microsoft.Extensions.Primitives;
using static Humanizer.In;
using static NLog.LayoutRenderers.Wrappers.ReplaceLayoutRendererWrapper;

namespace WebApi.Middleware
{
    public class SanitizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHtmlSanitizer _htmlSanitizer;

        public SanitizationMiddleware(RequestDelegate next, IHtmlSanitizer htmlSanitizer)
        {
            _next = next;
            _htmlSanitizer = htmlSanitizer;
        }

        // Sanitize request data, such as form inputs, query parameters, and route values
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.HasFormContentType)
            {
                var form = context.Request.Form;

                var sanitizedForm = new FormCollection(
                    form.ToDictionary(
                        kvp => kvp.Key,
                        kvp => new StringValues(
                            kvp.Value.Select(value => _htmlSanitizer.Sanitize(value).ToString()).ToArray()
                        )
                    )
                );

                context.Request.Form = sanitizedForm;
            }

            await _next(context);
        }
    }

}
