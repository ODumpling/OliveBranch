using InertiaCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OliveBranch.Domain.Entities;
using OliveBranch.WebApp.Models;

namespace OliveBranch.WebApp.Controllers;

public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(SignInManager<ApplicationUser> signInManager, ILogger<AuthController> logger,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _logger = logger;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> LoginAsync(string returnUrl = "/")
    {
        var isAuthenticated = HttpContext.User.Identity!.IsAuthenticated;

        if (isAuthenticated) return Inertia.Location(returnUrl);
        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        return Inertia.Render("Auth/Login", new
        {
            returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] InputModel input)
    {
        var returnUrl = input.ReturnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Inertia.Render("Auth/Login", new
                {
                    returnUrl
                });
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(user, input.Password, input.RememberMe, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return Inertia.Location(returnUrl);
            }

            if (result.RequiresTwoFactor)
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, input.RememberMe });
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Inertia.Render("Auth/Login", new
            {
                returnUrl
            });
        }

        return Inertia.Render("Auth/Login", new
        {
            returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");

        return Redirect("/auth/login");
    }
}