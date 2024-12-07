using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Queries.List;


public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllUsersAsync();
        return users.Select(u => new UserResponse(u.UserId, u.Role, u.Name, u.Surname, EventResponse.CreateEventResponse(u.Events!)));
    }
}
