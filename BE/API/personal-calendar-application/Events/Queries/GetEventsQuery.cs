using MediatR;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Queries;



public record GetEventsQuery(Guid UserId) : IRequest<IEnumerable<EventResponse>>; 