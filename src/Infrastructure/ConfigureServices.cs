using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OliveBranch.Application.Common.Interfaces;
using OliveBranch.Domain.Entities;
using OliveBranch.Infrastructure.Common;
using OliveBranch.Infrastructure.Data;
using OliveBranch.Infrastructure.Identity;

namespace OliveBranch.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Missing connection string");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)).EnableSensitiveDataLogging());


        services.AddIdentity<ApplicationUser, ApplicationRole>(o => { o.Stores.MaxLengthForKeys = 128; })
            .AddEntityFrameworkStores<AppDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Auth/AccessDenied";
            options.Cookie.Name = "_OliveBranch.Auth";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/Auth/Login";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });


        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());

        services.AddScoped<DatabaseInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IIdentityService, IdentityService>();


        return services;
    }
}