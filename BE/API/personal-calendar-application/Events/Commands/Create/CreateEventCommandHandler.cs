using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_domain.Models;

namespace personal_calendar_application.Events.Commands.Create;


public class CreateEventCommandHandler(IEventRepository eventRepository) : IRequestHandler<CreateEventCommand, EventResponse?>
{
    private readonly IEventRepository eventRepository = eventRepository;

    public async Task<EventResponse?> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.EventName)) return null;
        var ev = Event.CreateEvent(
            request.EventName,
            request.Description,
            request.EventStart,
            request.EventEnd,
            request.Location,
            request.UserId);
        var newEvent = await eventRepository.CreateEventAsync(ev);
        if (newEvent is null) return null;
        return new EventResponse(
            newEvent.EventId,
            newEvent.EventName,
            newEvent.Description,
            newEvent.EventStart,
            newEvent.EventEnd,
            newEvent.Location);
    }
}
