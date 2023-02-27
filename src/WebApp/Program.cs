using InertiaCore.Extensions;
using OliveBranch.Application;
using OliveBranch.Infrastructure;
using OliveBranch.Infrastructure.Data;
using OliveBranch.WebApp.Middleware;
using OliveBranch.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebAppServices();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddInertia();

builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();

    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitialiser>();
    await initializer.InitialiseAsync();
    await initializer.SeedAsync();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseInertia();

app.UseInertiaAuth();

app.UseInertiaXSRF();

app.Run();