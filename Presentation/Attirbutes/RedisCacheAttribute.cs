using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Presentation.Attirbutes;

public class RedisCacheAttribute(int durationInSeconds = 90)
    : ActionFilterAttribute
{
    // Action filter
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
        // 1. Get key
        string cacheKey = CreateCacheKey(context.HttpContext.Request);
        //  2. Search with key
        var cacheValue = await cacheService.GetAsync(cacheKey);
        // if there is a value return  database entry else  call function and save return value in database
        if (cacheValue != null)
        {
            //return from cache
            context.Result = new ContentResult
            {
                Content = cacheValue,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK,
            };
            return;
        }

        // call action
        var executedContext = await next.Invoke();
        // if status code is 200 ok
        if (executedContext.Result is OkObjectResult result)
        {
            await cacheService.SetAsync(cacheKey,
                result.Value,
                TimeSpan.FromSeconds(durationInSeconds)
            );
        }
    }

    private static string CreateCacheKey(HttpRequest request)
    {
        StringBuilder builder = new();
        builder.Append(request.Path.Value);
        if (request.Query.Any()) builder.Append('?');

        foreach (var kv in request.Query.OrderBy(kv => kv.Key))
            builder.Append($"{kv.Key}={kv.Value}&");

        return builder.ToString().Trim('&');
    }
}