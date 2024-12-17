import { useEffect, useState } from "react";
import request from "./request";
import { useLocation } from "react-router-dom";


const ShareEvents = () => {
    const params = useLocation();
    const [events, setEvents] = useState(null);
    const [name, setName] = useState(null);
    const [error, setError] = useState(false);

    useEffect(() => {
        const getEvents = async function () {
            const response = await request("GET", `http://localhost:5183/api/shareevents/${params.search}`);
            setEvents(response.data.events);
            setName(response.data.name);
            setError(response.error);
        }
        getEvents();
    }, [params])




    return (
        <div>
            {name && <h1>{name}'s Events</h1>}
            {events && events.map((event) => (
                <div key={event.eventId}>
                    <p>Event Name: {event.eventName}</p>
                    {event.description && <p>Description: {event.description}</p>}
                    {event.location && <p>Location: {event.location}</p>}
                    <p>Event Start: {event.eventStart}</p>
                    <p>Event End: {event.eventEnd}</p>
                </div>
            )
            )}
            {events.length === 0 && <p>No Events</p>}
            {error && <p>An error occurred</p>}
            {error && <p>{error}</p>}
        </div>

    );

}


export default ShareEvents;