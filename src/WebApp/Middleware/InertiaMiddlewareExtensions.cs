using System.Security.Claims;
using InertiaCore;
using Microsoft.AspNetCore.Antiforgery;

namespace WebApp.Middleware;

public static class InertiaMiddlewareExtensions
{
    /// <summary>
    /// Adds current user details as shared data across requests. 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInertiaAuth(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = context.User?.FindFirstValue(ClaimTypes.Name);
            var email = context.User?.FindFirstValue(ClaimTypes.Email);

            Inertia.Share("auth", new
            {
                IsAuthenicated = context.User?.Identity?.IsAuthenticated,
                User = new
                {
                    Id = userId,
                    Username = username,
                    Email = email
                }
            });

            await next(context);
        });


        return app;
    }

    /// <summary>
    /// Creates a cookie to be used to validate against CSRF or XSRF attacks
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseInertiaXSRF(this IApplicationBuilder app)
    {
        var antiforgery = app.ApplicationServices.GetRequiredService<IAntiforgery>();

        app.Use((context, next) =>
        {
            if (context.Request.Cookies["XSRF-TOKEN"] != null) return next(context);

            var tokenSet = antiforgery.GetAndStoreTokens(context);

            context.Response.Cookies.Append("XSRF-TOKEN", tokenSet.RequestToken!,
                new CookieOptions
                {
                    HttpOnly = false
                });

            return next(context);
        });


        return app;
    }
}