using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IAuthService
{
    Task<string> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default);
    Task<(string AccessToken, DateTime ExpirationDate)> GenerateAccessTokenAsync(ApplicationUser user, IList<string>? roles, CancellationToken cancellationToken = default);
}
