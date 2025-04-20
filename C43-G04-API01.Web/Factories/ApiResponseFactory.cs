using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace C43_G04_API01.Web.Factories;

public static class ApiResponseFactory
{
    public static IActionResult GenerateApiValidationResponse(ActionContext context)
    {
        // Get the entries in model state that have validation errors
        var errors = context.ModelState.Where(m =>
                m.Value.Errors.Any())
            .Select(e => new ValidationError()
            {
                Field = e.Key,
                Errors = e.Value.Errors.Select(error => error.ErrorMessage)
            });
        var response = new ValidationErrorResponse() { ValidationErrors = errors };
        return new BadRequestObjectResult(response);
    }
}