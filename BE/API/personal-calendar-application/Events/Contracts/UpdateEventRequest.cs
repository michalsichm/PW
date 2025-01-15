namespace personal_calendar_application.Events.Contracts;


public record UpdateEventRequest(
    Guid EventId,
    string EventName,
    string? Description,
    DateTime EventStart,
    DateTime EventEnd,
    string? Location,
    Guid UserId
);