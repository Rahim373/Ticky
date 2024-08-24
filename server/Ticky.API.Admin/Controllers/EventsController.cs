using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Ticky.Application.Commands.Events;
using Ticky.Application.Queries.Events;
using Ticky.Domain.Entities;

namespace Ticky.API.Admin.Controllers;

public class EventsController : BaseApiController
{
    private readonly ISender _sender;

    public EventsController(ISender sender)
    {
        _sender = sender;
    }

    #region Events

    [HttpGet]
    [Tags("Events")]
    public Task<IActionResult> GetEventAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Returns detail of an particular event.
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    [HttpGet("{eventId:guid}")]
    [Tags("Events")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(Event))]
    public async Task<IActionResult> GetEventsAsync(Guid eventId)
    {
        var response = await _sender.Send(new GetEventQuery(eventId));
        return response.Match(
            @event => new OkObjectResult(@event),
            Problem);
    }

    [HttpPost]
    [Tags("Events")]
    public async Task<IActionResult> CreateEventAsync(CreateEventCommand command)
    {
        var response = await _sender.Send(command);
        return response.Match(x => Ok(), Problem);
    }

    [HttpPut("{eventId:guid}")]
    [Tags("Events")]
    public Task<IActionResult> UpdateEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{eventId:guid}/invite-admin")]
    [Tags("Events")]
    public Task<IActionResult> InviteAdminToEventAsync(Guid eventId)
    {
        throw new NotImplementedException();
    }

    #endregion

    //#region Tickets

    //[HttpGet("{eventId:guid}/tickets")]
    //[Tags("Tickets")]
    //public Task<IActionResult> GetTicketsAsync(Guid eventId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpGet("{eventId:guid}/tickets/{ticketId:guid}")]
    //[Tags("Tickets")]
    //public Task<IActionResult> GetTicketAsync(Guid eventId, Guid ticketId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPost("{eventId:guid}/tickets")]
    //[Tags("Tickets")]
    //public Task<IActionResult> CreateTicketAsync(Guid eventId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPut("{eventId:guid}/tickets/{ticketId:guid}")]
    //[Tags("Tickets")]
    //public Task<IActionResult> UpdateTicketAsync(Guid eventId, Guid ticketId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpDelete("{eventId:guid}/tickets/{ticketId:guid}")]
    //[Tags("Tickets")]
    //public Task<IActionResult> DeleteTicketAsync(Guid eventId, Guid ticketId)
    //{
    //    throw new NotImplementedException();
    //}

    //#endregion

    //#region Discounts

    //[HttpGet("{eventId:guid}/discounts")]
    //[Tags("Discounts")]
    //public Task<IActionResult> GetDiscountsAsync(Guid eventId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpGet("{eventId:guid}/discounts/{discountId:guid}")]
    //[Tags("Discounts")]
    //public Task<IActionResult> GetDiscountAsync(Guid eventId, Guid discountId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPost("{eventId:guid}/discounts")]
    //[Tags("Discounts")]
    //public Task<IActionResult> CreateDiscountsAsync(Guid eventId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpPut("{eventId:guid}/discounts/{discountId:guid}")]
    //[Tags("Discounts")]
    //public Task<IActionResult> UpdateDiscountsAsync(Guid eventId, Guid discountId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpDelete("{eventId:guid}/discounts/{discountId:guid}")]
    //[Tags("Discounts")]
    //public Task<IActionResult> DeleteDiscountAsync(Guid eventId, Guid discountId)
    //{
    //    throw new NotImplementedException();
    //}

    //#endregion

    //#region Attendees

    //[HttpGet("{eventId:guid}/attendees")]
    //[Tags("Attendees")]
    //public Task<IActionResult> GetAttendeesAsync(Guid eventId)
    //{
    //    throw new NotImplementedException();
    //}

    //[HttpGet("{eventId:guid}/attendees/{attendeeId:guid}")]
    //[Tags("Attendees")]
    //public Task<IActionResult> GetAttendeeAsync(Guid eventId, Guid attendeeId)
    //{
    //    throw new NotImplementedException();
    //}

    //#endregion
}
