using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OliveBranch.Application.Common.Interfaces;

namespace OliveBranch.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? string.Empty;


        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} - {@Request}",
            requestName, userId, request);

        return Task.CompletedTask;
    }
}