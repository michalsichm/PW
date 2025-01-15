using MediatR;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Commands.Create;


public record CreateUserCommand
(
    string Name,
    string Surname,
    string Email,
    string Password,
    string Role
) : IRequest<UserResponse>
{
    public static CreateUserCommand CreateCommand(CreateUserOrAdminRequest request)
    {
        return new CreateUserCommand(request.Name.Trim(), request.Surname.Trim(),
            request.Email.ToLower().Trim(),
            request.Password.Trim(), request.Role);
    }
}


