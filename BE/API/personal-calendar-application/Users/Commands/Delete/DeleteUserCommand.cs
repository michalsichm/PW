using MediatR;

namespace personal_calendar_application.Users.Commands.Delete;

public record DeleteUserCommand(Guid UserId) : IRequest;