using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using personal_calendar_application.Events.Commands.Create;
using personal_calendar_application.Events.Commands.Delete;
using personal_calendar_application.Events.Commands.Update;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Events.Queries;

namespace personal_calendar_presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class EventsController : ControllerBase
{
    private readonly ISender sender;

    public EventsController(ISender sender)
    {
        this.sender = sender;
    }


    // [HttpGet]
    // public async Task<IActionResult> GetAllEvents()
    // {
    //     var ev = await _eventService.GetAllEventsAsync();
    //     return Ok(ev);
    // }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        var ev = await sender.Send(new GetEventQuery(id));
        return ev is null ? NotFound() : Ok(ev);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvent(CreateEventRequest request)
    {
        var command = CreateEventCommand.CreateCommand(request);
        var createdEvent = await sender.Send(command);
        if (createdEvent is null) return NotFound(new { message = $"User with id {request.UserId} not found" });
        return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventId }, createdEvent);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEvent(UpdateEventRequest request)
    {
        if (request.EventEnd < request.EventStart)
        {
            return UnprocessableEntity(new { message = "Invalid time span" });
        }
        var command = UpdateEventCommand.CreateCommand(request);
        var updated_event = await sender.Send(command);
        if (updated_event is null)
        {
            return NotFound(new { message = $"Event with id {request.EventId} not found" });
        }
        return Ok(updated_event);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        await sender.Send(new DeleteEventCommand(id));
        return NoContent();
    }
}