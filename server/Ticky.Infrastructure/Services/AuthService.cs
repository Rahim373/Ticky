using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticky.Application.Common.Interfaces;
using Ticky.Shared.Settings;

namespace Ticky.Infrastructure.Services;

internal class AuthService : IAuthService
{
    private readonly Jwtconfig _jwtConfig;

    public AuthService(IOptions<ApplicationOptions> options)
    {
        _jwtConfig = options.Value.JwtConfig;
    }

    public Task<string> GenerateRefreshTokenAsync(CancellationToken cancellationToken = default)
    {
        var token = Guid.NewGuid().ToString().Replace("-", "");
        return Task.FromResult(token);
    }

    public async Task<(string AccessToken, DateTime ExpirationDate)> GenerateAccessTokenAsync(Guid userId, string email, CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var expireDate = now.AddMinutes(_jwtConfig.AccessTokenExpiryTimeMins);


        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: expireDate,
            signingCredentials: credentials
        );

        return await Task.FromResult((new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), expireDate));
    }
}
