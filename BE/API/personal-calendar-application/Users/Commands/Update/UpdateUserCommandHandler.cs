using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;
using personal_calendar_presentation.Contracts;

namespace personal_calendar_application.Users.Commands.Update;


public class UpdateUserCommandHandler(IUserRepository userRepository, IHashService hashService) : IRequestHandler<UpdateUserCommand, UserResponse?>
{
    // private readonly IUserRepository userRepository;

    private readonly IUserRepository userRepository = userRepository;
    private readonly IHashService hashService = hashService;
    // public UpdateUserCommandHandler(IUserRepository userRepository)
    // {
    //     this.userRepository = userRepository;
    // }

    public async Task<UserResponse?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Surname)) return null;
        // if (request.OldPassword is null && request.NewPassword is not null || request.OldPassword is not null && request.NewPassword is null) return null;
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user is null) return null;
        // if (request.OldPassword is not null && request.NewPassword is not null)
            // if ((request.OldPassword == null) ^ (request.NewPassword == null)) return null;
        if (request.NewPassword is not null && request.OldPassword is not null)
        {
            // validate here
            if (!hashService.Validate(request.OldPassword!, user.Password!)) return null;
            user.UpdateUser(request.Name, request.Surname, hashService.HashPassword(request.NewPassword!));
            // Console.WriteLine("Changed Password");
            await userRepository.UpdateUserAsync(user);
            return new UserResponse(user.UserId, user.Role, user.Name, user.Surname);

        }
        // var user = await userRepository.GetUserByIdAsync(request.UserId);
        // if (user is null) return null;
            // Console.WriteLine("Changed Password");
        user.UpdateUser(request.Name, request.Surname);
        await userRepository.UpdateUserAsync(user);
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname);

    }
}