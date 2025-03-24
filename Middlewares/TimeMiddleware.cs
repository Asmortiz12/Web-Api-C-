namespace Web_Api.Middlewares;
public class TimeMiddleware
{
    private readonly RequestDelegate next;

    public TimeMiddleware(RequestDelegate nextRequest)
    {
        next = nextRequest;

    }

    public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context)
    {
       
        await next(context);

        if (context.Request.Query.Any(p => p.Key == "time"))
        {
            await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
        }
    }
}
    public static class TimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMiddleware>();
        }
    }