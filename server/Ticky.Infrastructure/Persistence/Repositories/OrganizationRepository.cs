using Microsoft.EntityFrameworkCore;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly ApplicationDbContext _context;

    public OrganizationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Organization?> GetOrganizationByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Organizations
            .Where(x => x.Owner.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<(int Total, List<Organization> Items)> GetOrganizationsAsync(int take, int skip, CancellationToken cancellationToken = default)
    {
        var query = _context.Organizations;

        var count = await query.CountAsync();
        var data = await query
            .Include(x => x.Owner)
            .OrderBy(x => x.CreatedOn)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

        return await Task.FromResult((count, data));
    }
}
