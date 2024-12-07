using MediatR;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Commands.Update;


public record UpdateEventCommand(
    Guid EventId,
    string? EventName,
    string? Description,
    DateTime EventStart,
    DateTime EventEnd,
    string? Location,
    Guid UserId
) : IRequest<EventResponse>
{
    public static UpdateEventCommand CreateCommand(UpdateEventRequest request)
    {
        return new UpdateEventCommand(
            request.EventId,
            request.EventName,
            request.Description,
            request.EventStart,
            request.EventEnd,
            request.Location,
            request.UserId);
    }

}
