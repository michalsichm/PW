using MediatR;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_presentation.Contracts;


public record UpdateUserCommand
(
    Guid UserId,
    string Name,
    string Surname,
    string? OldPassword = null,
    string? NewPassword = null
) : IRequest<UserResponse>
{
    public static UpdateUserCommand CreateCommand(UpdateUserRequest request)
    {
        if (request.OldPassword is not null && request.NewPassword is not null)
        {
            return new UpdateUserCommand(request.UserId, request.Name, request.Surname, request.OldPassword, request.NewPassword);
        }
        return new UpdateUserCommand(request.UserId, request.Name, request.Surname);
    }
}