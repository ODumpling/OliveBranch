using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OliveBranch.Domain.Entities;

namespace OliveBranch.Application.Auth.Commands.LogoutUserCommand;

public record LogoutUserCommand() : IRequest;


public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly ILogger<LogoutUserCommandHandler> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutUserCommandHandler(SignInManager<ApplicationUser> signInManager, ILogger<LogoutUserCommandHandler> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
    }
}

