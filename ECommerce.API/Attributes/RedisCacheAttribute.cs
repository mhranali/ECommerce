using ECommerce.UseCases.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace ECommerce.API.Attributes;

public class RedisCacheAttribute : ActionFilterAttribute
{
    private readonly int durationInSec;

    public RedisCacheAttribute(int durationInSec = 60)
    {
        this.durationInSec = durationInSec;
    }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Get the cache service from DI container
        var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheServices>();

        var cacheKey = CreateCacheKey(context.HttpContext.Request);

        var data = await cacheService.GetAsync(cacheKey);

        // if data exists in cache, get data from cache and skip endpoint

        if (!string.IsNullOrEmpty(data))
        {
            context.Result = new ContentResult()
            {
                Content = data,
                ContentType = "application/json",
                StatusCode = StatusCodes.Status200OK

            };
            return;
        }

        // if data does not exist in cache, execute endpoint and cache the response
         var Executed = await next.Invoke();
        if (Executed.Result is OkObjectResult { Value : not null} ok)
        {
            await cacheService.SetAsync(cacheKey, ok.Value, TimeSpan.FromSeconds(durationInSec));
        }
    }


    private static string CreateCacheKey(HttpRequest request)
    {
        var key = new StringBuilder();
        key.Append(request.Path);

        if (request.Query.Any())
        { 
            key.Append("?");
            foreach (var (k, v) in request.Query)
            {
                key.Append(k).Append('=').Append(v).Append('&');
                
            }
        }
        return key.ToString();
    }
}
