using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;
using personal_calendar_presentation.Contracts;

namespace personal_calendar_application.Users.Commands.Update;


public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse?>
{
    private readonly IUserRepository userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserResponse?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user is null) return null;
        user.UpdateUser(request.Name, request.Surname, request.Email);
        await userRepository.UpdateUserAsync(user);
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname, EventResponse.CreateEventResponse(user.Events!));

    }
}