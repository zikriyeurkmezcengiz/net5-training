using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public class HelloMiddleware
{
    readonly RequestDelegate _next;
    public HelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine("Hello 1");
        await _next.Invoke(context);
        Console.WriteLine("Hello 2");
    }
}

static public class HelloMiddlewareExtension
{
    public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloMiddleware>();
    }
}