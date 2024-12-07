using personal_calendar_domain.Models;

namespace personal_calendar_application.Events.Contracts;


public record EventResponse(
    Guid EventId,
    string? EventName,
    string? Description,
    DateTime EventStart,
    DateTime EventEnd,
    string? Location
)
{
    public static ICollection<EventResponse> CreateEventResponse(List<Event> events)
    {
        return events.Select(ev => new EventResponse(ev.EventId, ev.EventName, ev.Description, ev.EventStart, ev.EventEnd, ev.Location)).ToList();
    }
};