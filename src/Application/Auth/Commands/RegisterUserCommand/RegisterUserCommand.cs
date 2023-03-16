using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Application.Auth.Commands.RegisterUserCommand;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    public string Username { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string ConfirmPassword { get; init; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            return IdentityResult.Failed(new IdentityError(){Code = "500",Description = "Password is not the same as confirmation password."});
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result;
    }
}