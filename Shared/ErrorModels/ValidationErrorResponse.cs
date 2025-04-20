using System.Net;

namespace Shared.ErrorModels;

public class ValidationErrorResponse // BadRequest
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string ErrorMessage { get; set; } = "Validation Failed";
    public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
}

public class ValidationError()
{
    public string Field { get; set; } = default!;
    public IEnumerable<string> Errors { get; set; } = [];
}