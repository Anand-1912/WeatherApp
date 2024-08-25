using Microsoft.Extensions.Primitives;
using Serilog.Context;

namespace WeatherApp.Api.Middlewares;
public class RequestContextLoggingMiddleware
{
    private const string CorrelationIdHeaderName = "Correlation-Id";
    private readonly RequestDelegate _next;
    public RequestContextLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public Task Invoke(HttpContext context)
    {
        string correlationId = GetCorrelationId(context);

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            return _next.Invoke(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {     
        context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out StringValues correlationId);
        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
public static class RequestContextLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app) =>
        app.UseMiddleware<RequestContextLoggingMiddleware>();
}
