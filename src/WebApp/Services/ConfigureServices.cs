using Application.Common.Interfaces;

namespace WebApp.Services;

public static class ConfigureServices
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks();

        return services;
    }
}