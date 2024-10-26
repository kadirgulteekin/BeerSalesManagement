using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace Web.Api.OutputCaching;

internal sealed class CustomPolicy : IOutputCachePolicy
{
    public CustomPolicy()
    {

    }
    ValueTask IOutputCachePolicy.CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        bool attempOutputCaching = AttemptOutputCaching(context);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = attempOutputCaching;
        context.AllowCacheStorage = attempOutputCaching;
        context.AllowLocking = true;
        return ValueTask.CompletedTask;
    }

    public ValueTask ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }

    ValueTask IOutputCachePolicy.ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        HttpResponse response = context.HttpContext.Response;
        if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
        {
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }
        if (response.StatusCode != StatusCodes.Status200OK)
        {
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }
        return ValueTask.CompletedTask;
    }

    private static bool AttemptOutputCaching(OutputCacheContext context)
    {
        HttpRequest request = context.HttpContext.Request;
        if (!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method))
        {
            return false;
        }
        return true;
    }
}