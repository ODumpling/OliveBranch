using InertiaCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace OliveBranch.WebApp.Middleware;

public class CsrfMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IAntiforgery _antiforgery;

    public CsrfMiddleware(RequestDelegate next, IAntiforgery antiforgery)
    {
        _next = next;
        _antiforgery = antiforgery;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request method is POST
        if (HttpMethods.IsPost(context.Request.Method) || HttpMethods.IsPut(context.Request.Method) || HttpMethods.IsPatch(context.Request.Method) || HttpMethods.IsDelete(context.Request.Method))
        {
            context.Response.StatusCode = 419;
            // Get the X-CSRF-TOKEN header
            string csrfToken = context.Request.Headers["X-XSRF-TOKEN"]!;

            // Validate the CSRF token
            if (!_antiforgery.IsRequestValidAsync(context).Result)
            {
                // Return a 419 status code if the CSRF token is invalid
                context.Response.StatusCode = 419;
                return;
            }
        }
        else if (context.Request.Method == "GET")
        {
            // Generate a new CSRF token
            var csrfToken = _antiforgery.GetAndStoreTokens(context).RequestToken;

            // Set the X-CSRF-TOKEN cookie
            context.Response.Cookies.Append("XSRF-TOKEN", csrfToken, new CookieOptions
            {
                HttpOnly = false,
                SameSite = SameSiteMode.Strict,
                Secure = true,
                MaxAge = TimeSpan.FromMinutes(20)
            });
        }

        await _next(context);
    }
}