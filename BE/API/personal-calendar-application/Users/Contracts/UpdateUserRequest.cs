namespace personal_calendar_application.Users.Contracts;


public record UpdateUserRequest(Guid UserId, string Name, string Surname, string? OldPassword, string? NewPassword);