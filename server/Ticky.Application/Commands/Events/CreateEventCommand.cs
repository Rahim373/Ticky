using ErrorOr;
using FluentValidation;
using MediatR;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
using Ticky.Domain.Enums;

namespace Ticky.Application.Commands.Events;

public record CreateEventCommand(string Name, string Description, EventStatus Status)
    : IRequest<ErrorOr<Event>>;


public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Description).NotEmpty().NotNull();
        RuleFor(x => x.Status).IsInEnum();
    }
}


public class CreateEventCommandHnadler
    : IRequestHandler<CreateEventCommand, ErrorOr<Event>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserContext _userContext;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEventCommandHnadler(
        IEventRepository eventRepository,
        IUserContext userContext,
        IUnitOfWork unitOfWork)
    {
        _eventRepository = eventRepository;
        _userContext = userContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Event>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.GetUserId();

        if (userId is null)
        {
            return Error.Unauthorized("Invalid user id");
        }

        var @event = new Event(
            name: request.Name,
            description: request.Description,
            status: request.Status,
            createdByUserId: userId.Value
        );

        await _eventRepository.CreateEventAsync(@event, cancellationToken);
        await _unitOfWork.CommitChangesAsync();

        return @event;
    }
}
