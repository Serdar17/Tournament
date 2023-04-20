using Tournament.Middleware;

namespace Tournament.Extensions;

public static class CustomExceptionHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandle(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandleMiddleware>();
    }
}