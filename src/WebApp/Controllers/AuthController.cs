using InertiaCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OliveBranch.Application.Auth.Commands.LoginUserCommand;
using OliveBranch.Application.Auth.Commands.LogoutUserCommand;
using OliveBranch.Application.Auth.Commands.RegisterUserCommand;

namespace OliveBranch.WebApp.Controllers;

public class AuthController : BaseController
{
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    public async Task<IActionResult> Register()
    {
        return Inertia.Render("Auth/Register");
    }


    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await Mediator.Send(command);

        if (result.Succeeded) return RedirectToAction("Login");
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return Inertia.Render("Auth/Register");

    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl = "/")
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
    public async Task<IActionResult> Login([FromBody] LoginUserCommand input)
    {
        var returnUrl = input.ReturnUrl;

        if (!ModelState.IsValid)
        {
            return Inertia.Render("Auth/Login", new
            {
                returnUrl
            });
        }

        var result = await Mediator.Send(input);

        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in");
            return Inertia.Location(returnUrl);
        }

        if (result.RequiresTwoFactor)
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, input.RememberMe });
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out");
            return RedirectToPage("./Lockout");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        _logger.LogInformation("Failed login attempt with email {Email}", input.Email);
        return Inertia.Render("Auth/Login", new
        {
            returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await Mediator.Send(new LogoutUserCommand());

        return Redirect("/auth/login");
    }
}