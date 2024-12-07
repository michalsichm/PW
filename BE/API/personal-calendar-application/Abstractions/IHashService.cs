namespace personal_calendar_application.Abstractions;


public interface IHashService
{
    bool Validate(string password, string hashedPassword);
    string HashPassword(string password);
}