using System.Text.Json.Serialization;

namespace personal_calendar_domain.Models;

public class Event
{
    public Guid EventId { get; private set; }
    public string? EventName { get; private set; }
    public string? Description { get; private set; }
    public DateTime EventStart { get; private set; }
    public DateTime EventEnd { get; private set; }
    public string? Location { get; private set; }
    public DateTime Created { get; private set; }
    public DateTime Updated { get; private set; }
    public Guid UserId { get; private set; }
    [JsonIgnore]
    public User? User { get; private set; }


    private Event() { }

    public Event(
        string? eventName,
        string? description,
        DateTime eventStart,
        DateTime eventEnd,
        string? location,
        Guid userId)
    {
        EventId = Guid.NewGuid();
        EventName = eventName;
        Description = description;
        EventStart = eventStart;
        EventEnd = eventEnd;
        Location = location;
        Created = DateTime.UtcNow;
        Updated = DateTime.UtcNow;
        UserId = userId;
    }


    public static Event CreateEvent(
        string eventName,
        string? description,
        DateTime eventStart,
        DateTime eventEnd,
        string? location,
        Guid userId)
    {
        // if (eventStart > eventEnd) return null; exception
        return new(eventName, description, eventStart, eventEnd, location, userId);
    }

    public void UpdateEvent(
        string? eventName,
        string? description,
        DateTime eventStart,
        DateTime eventEnd,
        string? location)
    {
        EventName = eventName;
        Description = description;
        EventStart = eventStart;
        EventEnd = eventEnd;
        Location = location;
        Updated = DateTime.UtcNow;
    }
}