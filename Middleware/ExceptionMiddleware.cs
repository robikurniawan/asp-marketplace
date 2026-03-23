using System.Net;
using System.Text.Json;

namespace AspMarketplace.Web.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var isAjax = context.Request.Headers.XRequestedWith == "XMLHttpRequest";
        var acceptsJson = context.Request.Headers.Accept.ToString().Contains("application/json");

        if (isAjax || acceptsJson)
        {
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { success = false, message = "Terjadi kesalahan pada server." });
            await context.Response.WriteAsync(response);
        }
        else
        {
            context.Response.Redirect("/error");
        }
    }
}
