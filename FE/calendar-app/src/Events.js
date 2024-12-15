const Events = ({ events }) => {



    return (
        <div>
            {events && events.map(event => (
                <div key={event.eventId}>
                    <p>Event Name: {event.eventName}</p>
                    {event.description && <p>Description: {event.description}</p>}
                    {event.location && <p>Location: {event.location}</p>}
                    <p>Event Start: {event.eventStart}</p>
                    <p>Event End: {event.eventEnd}</p>
                </div>
            ))}
            {events !== null && events.length === 0 && <p>No Events</p>}
        </div>

    );
}

export default Events;