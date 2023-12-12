using Application.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Filters
{
    public class ValidationError
    {
        public string Field { get; set; }

        public string Message { get; set; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }

    public class ValidationResultModel : BaseResponse
    {
        public List<ValidationError> Data { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Message = "Validation Failed";
            Data = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                .ToList();
        }

        public ValidationResultModel(List<ValidationResult> results)
        {
            Message = "Validation Failed";
            List<ValidationError> data = new List<ValidationError>();
            foreach (ValidationResult result in results)
            {
                data.Add(new ValidationError(result.MemberNames.First(), result.ErrorMessage));
            }
            Data = data;
        }
    }
}
