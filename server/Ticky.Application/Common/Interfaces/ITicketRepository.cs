using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface ITicketRepository
{
    public Task<Ticket> GetTicketAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IList<Ticket>> GetTicketsAsync(CancellationToken cancellationToken = default);
    public Task<Ticket> CreateTicketAsync(Ticket ticket, CancellationToken cancellationToken = default);
    public Task<Ticket> UpdateTicketAsync(Guid id, Ticket ticket, CancellationToken cancellationToken = default);
    public Task<Ticket> DeleteTicketAsync(Guid id, Ticket ticket, CancellationToken cancellationToken = default);
}
