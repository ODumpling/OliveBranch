using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OliveBranch.Domain.Entities;

namespace OliveBranch.WebApp.Controllers;

public class UsersController : BaseController
{
    private readonly ILogger<UsersController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;

    /// <inheritdoc />
    public UsersController(SignInManager<ApplicationUser> signInManager, ILogger<UsersController> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public IActionResult Profile()
    {
        return Inertia.Render("Users/Profile", new
        {
        });
    }
}