using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;
using personal_calendar_domain.Models;
using personal_calendar_presentation.Contracts;

namespace personal_calendar_application.Users.Commands.Create;


public class CreateUserCommandHandler(IUserRepository userRepository, IHashService hashService) : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IHashService hashService = hashService;

    public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var hashedPassword = hashService.HashPassword(request.Password);
        var email = request.Email.ToLower();
        var user = new User(request.Name, request.Surname, email, hashedPassword, request.Role);
        await userRepository.CreateUserAsync(user);
        var eventsResponse = user.Role == "User"
           ? EventResponse.CreateEventResponse(user.Events!)
           : null;
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname, eventsResponse);

    }
}