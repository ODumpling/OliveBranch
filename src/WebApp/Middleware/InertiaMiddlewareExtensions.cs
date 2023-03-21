using System.Security.Claims;
using InertiaCore;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http.HttpResults;

namespace OliveBranch.WebApp.Middleware;

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
}