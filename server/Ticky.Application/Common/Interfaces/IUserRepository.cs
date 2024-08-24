using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IUserRepository
{
    ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}
