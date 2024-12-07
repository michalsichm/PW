using MediatR;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Queries.List;


public record GetUsersQuery : IRequest<IEnumerable<UserResponse>>;