using InertiaCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OliveBranch.WebApp.Controllers;

public class UsersController : BaseController
{
    private readonly ILogger<UsersController> _logger;
    private readonly SignInManager<IdentityUser> _signInManager;

    /// <inheritdoc />
    public UsersController(SignInManager<IdentityUser> signInManager, ILogger<UsersController> logger)
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