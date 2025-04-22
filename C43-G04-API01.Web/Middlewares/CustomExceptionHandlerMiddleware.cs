using System.Text.Json;
using Domain.Exceptions;
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
            // Logic
            await HandleNotFoundPathAsync(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Set status Code for the response
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // Set Content Type for the response
        context.Response.ContentType = "application/json";

        // Response Object
        var response = new ErrorDetails()
        {
            ErrorMessage = ex.Message,
        };
        response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };
        context.Response.StatusCode = response.StatusCode;

        // Return response as Json
        await context.Response.WriteAsJsonAsync(response);
    }

    private static async Task HandleNotFoundPathAsync(HttpContext context)
    {
        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                ErrorMessage = $"Path {context.Request.Path} not found",
                StatusCode = StatusCodes.Status404NotFound
            };
            await context.Response.WriteAsJsonAsync(response);

        }
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
    {
       app.UseMiddleware<CustomExceptionHandlerMiddleware>();
       return app;
    }
}