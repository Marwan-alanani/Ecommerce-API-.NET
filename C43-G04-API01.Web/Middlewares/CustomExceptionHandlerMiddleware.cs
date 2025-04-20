using System.Text.Json;
using Shared.ErrorModels;

namespace C43_G04_API01.Web.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next,
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            // Set status Code for the response
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // Set Content Type for the response
            context.Response.ContentType = "application/json";

            // Response Object
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message,
            };

            // Return response as Json
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}