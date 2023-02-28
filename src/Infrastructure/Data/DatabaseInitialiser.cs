using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Infrastructure.Data;

public class DatabaseInitialiser
{
    private readonly AppDbContext _context;
    private readonly ILogger<DatabaseInitialiser> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DatabaseInitialiser(ILogger<DatabaseInitialiser> logger, AppDbContext context,
        UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.MigrateAsync();
            } 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new ApplicationRole
        {
            Name = "Administrator",
        };

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
            await _roleManager.CreateAsync(administratorRole);

        // Default users
        var administrator = new ApplicationUser { UserName = "OliveAdministrator", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.Email != administrator.Email))
        {
            var result = await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });

            if (result.Succeeded)
            {
                _logger.LogInformation("Administator User Created");
            }
        }

        
        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            var user = await _userManager.FindByEmailAsync(administrator.Email);
            _context.TodoLists.Add(new TodoList
            {
                User = user,
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯" },
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" }
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}