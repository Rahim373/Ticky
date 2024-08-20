using Ticky.Domain.Entities;

namespace Ticky.Application.Interfaces.Persistence.Repositories;

public interface IEventRepository
{
    public ValueTask<Event?> GetEventAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Event> CreateEventAsync(Event @event, CancellationToken cancellationToken = default);
    public Task<Event> UpdateEventAsync(Guid id, Event @event, CancellationToken cancellationToken = default);
}
