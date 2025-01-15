namespace personal_calendar_application.Users.Contracts;


public record UserAdminResponse
(
    Guid UserId,
    string? Role,
    string? Name,
    string? Surname,
    DateTime Created,
    DateTime Updated
);