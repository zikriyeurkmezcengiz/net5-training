using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class CustomExceptionMiddleware
{
    RequestDelegate next;
    public CustomExceptionMiddleware(RequestDelegate _next)
    {
        next = _next;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
            Console.WriteLine(message);

            await next(context);

            watch.Stop();
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + "responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            Console.WriteLine(message);
        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleExceptionAsync(context, ex, watch);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, Stopwatch watch)
    {
        var code = HttpStatusCode.InternalServerError;
        string message = "[Error]    HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message: " + exception.Message + " in " + watch.Elapsed.TotalMilliseconds + " ms";
        Console.WriteLine(message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        var result = JsonConvert.SerializeObject(new { error = exception.Message }, Formatting.None);

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}