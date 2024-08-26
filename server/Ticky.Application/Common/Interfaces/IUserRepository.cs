using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<List<ApplicationRole>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}
