using Microsoft.EntityFrameworkCore;
using personal_calendar_application.Abstractions;
using personal_calendar_domain.Models;
using personal_calendar_infrastructure.Database;

namespace personal_calendar_infrastructure.Repositories;


public class EventRepository : IEventRepository
{
    private readonly CalendarDbContext _dbContext;

    public EventRepository(CalendarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Event?> CreateEventAsync(Event create_event)
    {
        bool userExists = _dbContext.Users.Any(u => u.UserId == create_event.UserId);
        if (userExists) return null;
        _dbContext.Events.Add(create_event);
        await _dbContext.SaveChangesAsync();
        return create_event;
    }

    public async Task<Event?> DeleteEventByIdAsync(Guid id)
    {
        var ev = await _dbContext.Events.FindAsync(id);
        // add appropriate error handling
        if (ev == null) return null;
        _dbContext.Events.Remove(ev);
        await _dbContext.SaveChangesAsync();
        return ev;
    }

    public async Task<Event?> GetEventByIdAsync(Guid id)
    {
        return await _dbContext.Events.FindAsync(id);
    }

    public async Task<Event> UpdateEventAsync(Event update_event)
    {
        // make sure event exists first??? or create one??
        // check datetime start datetime end
        _dbContext.Events.Update(update_event);
        await _dbContext.SaveChangesAsync();
        return update_event;
    }


    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _dbContext.Events.ToListAsync();
    }
}