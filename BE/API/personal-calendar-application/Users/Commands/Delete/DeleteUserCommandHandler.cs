using MediatR;
using personal_calendar_application.Abstractions;

namespace personal_calendar_application.Users.Commands.Delete;


public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await userRepository.DeleteUserByIdAsync(request.UserId);
    }
}