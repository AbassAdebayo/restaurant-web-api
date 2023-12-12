using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApi.Filters;
using static WebApi.Filters.ValidationError;

namespace WebApi.ActionResults
{
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
