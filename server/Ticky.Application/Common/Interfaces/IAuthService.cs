namespace Ticky.Application.Common.Interfaces;

public interface IAuthService
{
    Task<string> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default);
    Task<(string AccessToken, DateTime ExpirationDate)> GenerateAccessTokenAsync(Guid userId, string email, CancellationToken cancellationToken = default);
}
