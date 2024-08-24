namespace Ticky.Shared.ViewModels.Auth;

public record TokenResponse(
    Guid UserId,
    string Email,
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiration)
{
}
