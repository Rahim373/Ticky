using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
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

    public async Task<(string AccessToken, DateTime ExpirationDate)> GenerateAccessTokenAsync(ApplicationUser user, IList<string>? roles, CancellationToken cancellationToken = default)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;
        var expireDate = now.AddMinutes(_jwtConfig.AccessTokenExpiryTimeMins);


        List<Claim> claims = [
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),

        ];

        if (roles is not null)
        {
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expireDate,
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.FromResult((tokenHandler.WriteToken(token), expireDate));
    }
}
