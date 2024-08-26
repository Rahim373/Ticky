using Ticky.Domain.Entities;

namespace Ticky.Shared.ViewModels.Auth;

public record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiration,
    User User,
    Role[] Roles);

public record User(
    Guid Id,
    string Email,
    IList<string> Roles);

public record Role(
    Guid Id,
    string Name,
    Guid? Extends);

public static class ApplicationRoleExtensions
{
    public static Role ToRole(this ApplicationRole role)
    {
        return new Role(role.Id, role.Name!, role.Extends);
    }
}