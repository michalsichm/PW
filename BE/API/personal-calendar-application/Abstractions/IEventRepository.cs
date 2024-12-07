using personal_calendar_domain.Models;

namespace personal_calendar_application.Abstractions;

public interface IEventRepository
{
    Task<Event?> GetEventByIdAsync(Guid id);
    Task<Event?> CreateEventAsync(Event create_event);
    Task<Event> UpdateEventAsync(Event update_event);
    Task<Event?> DeleteEventByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetAllEventsAsync();
}