using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Users.Contracts;
using personal_calendar_domain.Models;

namespace personal_calendar_application.Users.Commands.Create;


public class CreateUserCommandHandler(IUserRepository userRepository, IHashService hashService) : IRequestHandler<CreateUserCommand, UserResponse?>
{
    private readonly IUserRepository userRepository = userRepository;
    private readonly IHashService hashService = hashService;

    public async Task<UserResponse?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Surname)) return null;
        if (await userRepository.IsEmailPresentInDb(request.Email)) return null;
        var hashedPassword = hashService.HashPassword(request.Password);
        // var email = request.Email.ToLower().Trim();
        var user = new User(request.Name, request.Surname, request.Email, hashedPassword, request.Role);
        await userRepository.CreateUserAsync(user);
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname);

    }

}