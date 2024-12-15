namespace personal_calendar_application.Events.Contracts;


public record CreateEventRequest(
    string EventName,
    string? Description,
    string? Location,
    DateTime EventStart,
    DateTime EventEnd,
    Guid UserId
);