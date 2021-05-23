using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using BookStoreWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class CustomExceptionMiddleware
{
    RequestDelegate next;
    ILoggerService _loggerService;
    public CustomExceptionMiddleware(RequestDelegate _next, ILoggerService loggerService)
    {
        next = _next;
        _loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();
        try
        {
            string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
            _loggerService.Write(message);

            await next(context);

            watch.Stop();
            message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + "responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
            _loggerService.Write(message);
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
        _loggerService.Write(message);

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