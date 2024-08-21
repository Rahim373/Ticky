using ErrorOr;
using MediatR;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Application.Queries.Events;

public record GetEventQuery(Guid eventId) 
    : IRequest<ErrorOr<Event>>;

public class GetEventQueryHandler 
    : IRequestHandler<GetEventQuery, ErrorOr<Event>>
{
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ErrorOr<Event>> Handle(
        GetEventQuery request, 
        CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetEventAsync(request.eventId); 

        if (@event is null)
        {
            return Error.NotFound(description: "Event not found.");
        }

        return @event;
    }
}