using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Repositories;

public class EmailJobRepository : IEmailJobRepository
{
    private readonly ApplicationDbContext _context;

    public EmailJobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task InsertEmailJobAsync(EmailJob emailJob, CancellationToken cancellationToken = default)
    {
        _context.EmailJobs.AddAsync(emailJob, cancellationToken);
        return Task.CompletedTask;
    }
}
