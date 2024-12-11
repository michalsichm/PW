using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Users.Commands.Create;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Services;


public sealed class AuthService : IAuthService
{
    private readonly IHashService hashService;
    private readonly IUserRepository userRepository;

    private readonly ISender sender;

    public AuthService(IHashService hashService, IUserRepository userRepository, ISender sender)
    {
        this.hashService = hashService;
        this.userRepository = userRepository;
        this.sender = sender;
    }

    public async Task<UserResponse?> Login(LoginRequest request)
    {
        var user = await userRepository.GetUserByEmail(request.Email.ToLower().Trim());
        if (user is null) return null; // asdf
        if (!hashService.Validate(request.Password.Trim(), user.Password!)) return null; // asdf
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname);
    }

    public async Task<UserResponse?> Register(CreateUserOrAdminRequest request)
    {
        bool emailPresent = await userRepository.IsEmailPresentInDb(request.Email.ToLower().Trim());
        if (emailPresent) return null;
        var command = CreateUserCommand.CreateCommand(request);
        var user = await sender.Send(command);
        return user;

    }

}
