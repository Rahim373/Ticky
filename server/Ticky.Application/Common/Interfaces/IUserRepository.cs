using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<List<ApplicationRole>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<RegistrationInvitation?> GetRegistrationInvitationTokenAsync(string token, CancellationToken cancellationToken = default);
    ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task InsertRegistrationInvitationAsync(string email, string token, DateTime expiresOn, bool organizationInvite, CancellationToken cancellationToken = default);
    Task ChangeInvitationUsedStatusAsync(bool isUsed, CancellationToken cancellationToken = default);
}
