using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OliveBranch.Application.Common.Exceptions;
using OliveBranch.Application.Common.Interfaces;
using OliveBranch.Domain.Entities;
using AuthorizeAttribute = OliveBranch.Application.Common.Security.AuthorizeAttribute;

namespace OliveBranch.Application.Common.Behaviours;

//Todo:: Redo Possibly
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;

    public AuthorizationBehaviour(
        ICurrentUserService currentUserService,
        UserManager<ApplicationUser> userManager,
        IAuthorizationService authorizationService,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory)
    {
        _currentUserService = currentUserService;
        _userManager = userManager;
        _authorizationService = authorizationService;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

        if (authorizeAttributes.Any())
        {
            // Must be authenticated user
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException();
            }

            // Role-based authorization
            await RoleBasedAuthorization(authorizeAttributes);

            // Policy-based authorization
            await PolicyBasedAuthorization(authorizeAttributes);
        }

        // User is authorized / authorization not required
        return await next();
    }

    private async Task PolicyBasedAuthorization(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
        if (authorizeAttributesWithPolicies.Any())
        {
            foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
            {
                var authorized = await AuthorizeAsync(_currentUserService.UserId, policy);

                if (!authorized)
                {
                    throw new ForbiddenAccessException();
                }
            }
        }
    }

    private async Task RoleBasedAuthorization(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

        if (authorizeAttributesWithRoles.Any())
        {
            var authorized = false;

            foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
            {
                foreach (var role in roles)
                {
                    var isInRole = await IsInRoleAsync(_currentUserService.UserId, role.Trim());
                    if (isInRole)
                    {
                        authorized = true;
                        break;
                    }
                }
            }

            // Must be a member of at least one role in roles
            if (!authorized)
            {
                throw new ForbiddenAccessException();
            }
        }
    }

    private async Task<bool> IsInRoleAsync(string? userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    private async Task<bool> AuthorizeAsync(string userId, string policy)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policy);

        return result.Succeeded;
    }
}