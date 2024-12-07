using MediatR;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Queries;


public record GetEventQuery(Guid EventId) : IRequest<EventResponse>;