import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "./AuthContext";
import { useState, useEffect } from "react";
import request from "./request";
import Navbar from "./Navbar";



const EventDetails = () => {
    const { id } = useParams();
    const [event, setEvent] = useState(null);
    const [error, setError] = useState(false);
    const [timeValid, setTimeValid] = useState();
    const { currentUser } = useAuth();
    const navigate = useNavigate();


    useEffect(() => {
        const loadData = async function () {
            const response = await request("GET", `http://localhost:5183/api/events/${id}`);
            setEvent(response.data);
            setError(response.error);
        }
        loadData();
    }, [id])


    useEffect(() => {
        if (!event) return;
        setTimeValid(!(new Date(event.eventStart) >= new Date(event.eventEnd)));
    }, [event])



    const handleDeleteEvent = async () => {
        await request("DELETE", `http://localhost:5183/api/events/${event.eventId}`)
        navigate('/');
    }


    const handleSubmit = async (e) => {
        e.preventDefault();
        // console.log(event);
        if (new Date(event.eventStart) >= new Date(event.eventEnd)) {
            alert("End cannot be earlier than start");
            return;
        }
        await request("PUT", `http://localhost:5183/api/events`, event, true);
        navigate('/');
    }




    if (currentUser.role === 'User') {
        return (
            <div>
                <Navbar />
                {event && <div>
                    <h2>Event Details</h2>
                    <form onSubmit={handleSubmit}>
                        <input
                            type="text"
                            placeholder="Event Name"
                            required
                            value={event.eventName}
                            onChange={(e) => setEvent(event => ({ ...event, eventName: e.target.value }))}
                        />
                        <input
                            type="text"
                            placeholder="Description"
                            value={event.description}
                            onChange={(e) => setEvent(event => ({ ...event, description: e.target.value }))}
                        />
                        <input
                            type="text"
                            placeholder="Location"
                            value={event.location}
                            onChange={(e) => setEvent(event => ({ ...event, location: e.target.value }))}
                        />

                        <label htmlFor="event-start">Event Start: </label>
                        <input value={event.eventStart}
                            onChange={(e) => setEvent(event => ({ ...event, eventStart: e.target.value }))}

                            type="datetime-local" id="event-start" name="event-start"></input>
                        <label htmlFor="event-end">Event End: </label>
                        <input required value={event.eventEnd}
                            onChange={(e) => setEvent(event => ({ ...event, eventEnd: e.target.value }))}
                            type="datetime-local" id="event-end" name="event-end"></input>
                        <button>Update Event</button>
                    </form>
                </div>}
                <button onClick={() => handleDeleteEvent()}>Delete Event</button>
                {!timeValid && <p>End cannot be earlier than start.</p>}
            </div>
        );
    }

    return (
        <div>
            <Navbar />
            {event &&
                <div>
                    <p>Event Name: {event.eventName}</p>
                    {event.description && <p>Description: {event.description}</p>}
                    {event.location && <p>Location: {event.location}</p>}
                    <p>Event Start: {event.eventStart}</p>
                    <p>Event End: {event.eventEnd}</p>
                </div>}
            {error && <p>An Error occurred</p>}
        </div>

    );


}


export default EventDetails;