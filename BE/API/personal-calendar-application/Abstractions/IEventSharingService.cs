using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Abstractions;


public interface IEventSharingService
{
    public string CreateLink(Guid id);
    public Task<UserResponse?> ReturnUserIfValid(string token, string parameters, Guid id);
}