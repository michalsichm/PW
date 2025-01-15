using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Queries.Get;


public class GetUserQueryAdminHandler : IRequestHandler<GetUserAdminQuery, UserAdminResponse?>
{
    private readonly IUserRepository userRepository;

    public GetUserQueryAdminHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<UserAdminResponse?> Handle(GetUserAdminQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserByIdAsync(request.UserId);
        if (user is null) return null;
        return new UserAdminResponse(user.UserId, user.Role, user.Name, user.Surname, user.Created, user.Updated);

    }
}