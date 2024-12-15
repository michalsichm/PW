using MediatR;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_presentation.Contracts;


public record UpdateUserCommand
(
    Guid UserId,
    string Name,
    string Surname
) : IRequest<UserResponse>
{
    public static UpdateUserCommand CreateCommand(UpdateUserRequest request)
    {
        return new UpdateUserCommand(request.UserId, request.Name, request.Surname);
    }
}