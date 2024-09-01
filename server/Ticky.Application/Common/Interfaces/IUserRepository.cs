using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<List<ApplicationRole>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<RegistrationInvitation?> GetRegistrationInvitationTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<RegistrationInvitation?> GetRegistrationInvitationTokenByEmailAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task InsertRegistrationInvitationAsync(string email, string token, DateTime expiresOn, bool organizationInvite, CancellationToken cancellationToken = default);
    Task ChangeInvitationUsedStatusAsync(Guid id, bool isUsed, CancellationToken cancellationToken = default);
    Task ChangeInvitationExpirationAsync(Guid id, DateTime expDate, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(Guid id, Expression<Func<SetPropertyCalls<ApplicationUser>, SetPropertyCalls<ApplicationUser>>> setExpression, CancellationToken cancellationToken = default);
}
