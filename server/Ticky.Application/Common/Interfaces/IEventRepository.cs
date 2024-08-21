using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IEventRepository
{
    ValueTask<Event?> GetEventAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Event> CreateEventAsync(Event @event, CancellationToken cancellationToken = default);
    Task<Event> UpdateEventAsync(Guid id, Event @event, CancellationToken cancellationToken = default);
}
