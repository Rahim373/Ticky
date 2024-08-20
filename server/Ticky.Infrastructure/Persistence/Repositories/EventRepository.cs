using Ticky.Application.Interfaces.Persistence.Repositories;
using Ticky.Domain.Entities;

namespace Ticky.Infrastructure.Persistence.Repositories;

internal class EventRepository : IEventRepository
{
    private readonly ApplicationDbContext _context;

    public EventRepository(ApplicationDbContext context)
    {
        this._context = context;
    }

    public Task<Event> CreateEventAsync(Event @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<Event?> GetEventAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Events.FindAsync(id);
    }

    public Task<Event> UpdateEventAsync(Guid id, Event @event, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
