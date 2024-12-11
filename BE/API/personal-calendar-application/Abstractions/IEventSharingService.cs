using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Abstractions;


public interface IEventSharingService
{
    public string CreateLink(Guid id);
    public Task<IEnumerable<EventResponse>?> ReturnEventsIfValid(string token, string parameters, Guid id);
}