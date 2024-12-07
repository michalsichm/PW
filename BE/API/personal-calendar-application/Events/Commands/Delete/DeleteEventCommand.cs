using MediatR;

namespace personal_calendar_application.Events.Commands.Delete;


public record DeleteEventCommand(Guid EventId) : IRequest;