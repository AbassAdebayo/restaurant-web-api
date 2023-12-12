using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Filters
{
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("Bearer ")
                },
                Required = true
            });
        }
    }
}
