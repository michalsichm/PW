namespace personal_calendar_application.Users.Contracts;


public record CreateUserOrAdminRequest(string Name, string Surname, string Email, string Password, string Role = "User");