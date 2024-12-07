using MediatR;
using personal_calendar_application.Abstractions;

namespace personal_calendar_application.Events.Commands.Delete;



public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        await _eventRepository.DeleteEventByIdAsync(request.EventId);
    }
}
