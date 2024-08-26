using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
using Throw;
using Microsoft.EntityFrameworkCore;

namespace Ticky.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<ApplicationRole>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        return _context.Roles.ToListAsync(cancellationToken);
    }

    public Task<RegistrationInvitation?> GetRegistrationInvitationTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        return _context.RegistrationInvitations.Where(x => x.Token == token).FirstOrDefaultAsync(cancellationToken);
    }

    public async ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));

        var user =  await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        return await ValueTask.FromResult(user);
    }

    public Task InsertRegistrationInvitationAsync(string email, string token, DateTime expiresOn, bool organizationInvite, CancellationToken cancellationToken = default)
    {
        _context.RegistrationInvitations.AddAsync(new RegistrationInvitation
        {
            CreatedOn = DateTime.UtcNow,
            Email = email,
            Token = token,
            ExpiresOn = expiresOn,
            IsUsed = false,
            OrganizationInvite = organizationInvite,
        }, cancellationToken);
        return Task.CompletedTask;
    }

    public Task ChangeInvitationUsedStatusAsync(bool isUsed, CancellationToken cancellationToken = default)
    {
        _context.RegistrationInvitations
            .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.IsUsed, isUsed), cancellationToken);
        return Task.CompletedTask;
    }
}
