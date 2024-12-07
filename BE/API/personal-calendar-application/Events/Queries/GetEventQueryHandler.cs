using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Queries;



public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventResponse?>
{
    private readonly IEventRepository _eventRepository;

    public GetEventQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var ev = await _eventRepository.GetEventByIdAsync(request.EventId);
        if (ev is null) return null;
        return new EventResponse(ev.EventId, ev.EventName, ev.Description, ev.EventStart, ev.EventEnd, ev.Location);
    }
}
