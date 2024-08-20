using Ticky.Domain.Entities;

namespace Ticky.Application.Interfaces.Persistence.Repositories;

public interface IAttendeeRepository
{
    public Task<EventAttendee> GetAttendeeAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IList<EventAttendee>> GetEventAttendeesAsync(CancellationToken cancellationToken = default);
}
