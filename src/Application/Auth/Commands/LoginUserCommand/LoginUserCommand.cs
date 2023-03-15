using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Application.Auth.Commands.LoginUserCommand;

public class LoginUserCommand : IRequest<SignInResult>
{
    [Required] [EmailAddress] public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Display(Name = "Remember me?")] public bool RememberMe { get; set; } = false;

    public string ReturnUrl { get; set; } = null!;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, SignInResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginUserCommandHandler> _logger;

    public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LoginUserCommandHandler> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<SignInResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            _logger.LogInformation("No user found with email: {Email}", request.Email);
            return SignInResult.Failed;
        }

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
        return result;
    }
}