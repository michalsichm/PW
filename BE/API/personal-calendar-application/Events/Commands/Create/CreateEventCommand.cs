using MediatR;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Commands.Create;


public record CreateEventCommand(
    string EventName,
    string? Description,
    DateTime EventStart,
    DateTime EventEnd,
    string? Location,
    Guid UserId
) : IRequest<EventResponse>
{
    public static CreateEventCommand CreateCommand(CreateEventRequest request)
    {
        return new CreateEventCommand(
            request.EventName.Trim(),
            request.Description?.Trim(),
            request.EventStart,
            request.EventEnd,
            request.Location?.Trim(),
            request.UserId);
    }

};