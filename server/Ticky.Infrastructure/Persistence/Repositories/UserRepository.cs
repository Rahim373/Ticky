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

    public async ValueTask<ApplicationUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));

        var user =  await _context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(cancellationToken);

        return await ValueTask.FromResult(user);
    }
}
