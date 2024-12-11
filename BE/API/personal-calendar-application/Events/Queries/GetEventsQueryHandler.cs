using MediatR;
using personal_calendar_application.Abstractions;
using personal_calendar_application.Events.Contracts;

namespace personal_calendar_application.Events.Queries;



public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventResponse>?>
{
    private readonly IUserRepository userRepository;

    public GetEventsQueryHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IEnumerable<EventResponse>?> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await userRepository.GetUserEventsByIdAsync(request.UserId);
        if (events is null) return null;
        return events.Select(e => new EventResponse(e.EventId, e.EventName, e.Description, e.EventStart, e.EventEnd, e.Location));
    }
}
