using MediatR;
using personal_calendar_application.Users.Contracts;

namespace personal_calendar_application.Users.Queries.Get;


public record GetUserQuery(Guid UserId) : IRequest<UserResponse>;