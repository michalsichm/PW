import { useState, useEffect } from "react";
import request from "./request";



const Events = ({ eventsList = null, id = null }) => {
    const [events, setEvents] = useState(eventsList);
    const [error, setError] = useState(null);


    useEffect(() => {
        if (eventsList) return;
        if (!eventsList && !id) {
            console.log("Neither id nor eventsList provided");
            return;
        }
        async function getSetData() {
            const { data, error } = await request("GET", `http://localhost:5183/api/users/${id}/events`, null, true);
            setEvents(data);
            setError(error);
        }
        getSetData();
    }, [id, eventsList]);



    return (
        <div>
            {events !== null && events && events.map(event => (
                <div key={event.eventId}>
                    <p>Event Name: {event.eventName}</p>
                    {event.description && <p>Description: {event.description}</p>}
                    {event.location && <p>Location: {event.location}</p>}
                    <p>Event Start: {event.eventStart}</p>
                    <p>Event End: {event.eventEnd}</p>
                </div>
            ))}
            {events !== null && events.length === 0 && <p>No Events</p>}
            {error && <p>An Error occurred</p>}
        </div>

    );
}

export default Events;