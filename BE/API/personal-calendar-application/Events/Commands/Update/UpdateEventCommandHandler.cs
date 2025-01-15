using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_domain.Models;

namespace personal_calendar_application.Events.Commands.Update;



public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventResponse?>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventResponse?> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {

        if (string.IsNullOrWhiteSpace(request.EventName)) return null;
        var ev = await _eventRepository.GetEventByIdAsync(request.EventId);
        if (ev is null) return null;
        ev.UpdateEvent(request.EventName, request.Description, request.EventStart, request.EventEnd, request.Location);
        await _eventRepository.UpdateEventAsync(ev);
        return new EventResponse(ev.EventId, ev.EventName, ev.Description, ev.EventStart, ev.EventEnd, ev.Location);
    }
}
