namespace personal_calendar_application.Users.Contracts;


public record UserResponse
(
    Guid UserId,
    string? Role,
    string? Name,
    string? Surname
);