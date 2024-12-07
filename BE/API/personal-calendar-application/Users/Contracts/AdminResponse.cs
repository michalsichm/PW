namespace personal_calendar_application.Users.Contracts;


public record AdminResponse
(
    Guid UserId,
    string? Role,
    string? Name,
    string? Surname
);