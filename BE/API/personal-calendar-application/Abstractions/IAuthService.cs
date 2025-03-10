using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Abstractions;


public interface IAuthService
{
    Task<UserResponse?> Register(CreateUserOrAdminRequest request);
    Task<UserResponse?> Login(LoginRequest request);
}