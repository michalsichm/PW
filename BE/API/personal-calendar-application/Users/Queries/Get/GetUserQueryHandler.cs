using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Queries.Get;


public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserResponse?>
{
    private readonly IUserRepository userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user is null) return null;
        // var events = await userRepository.GetUserByIdAsync(request.userId);
        // return event response
        return new UserResponse(user.UserId, user.Role, user.Name, user.Surname);

    }
}