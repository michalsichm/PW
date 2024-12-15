import request from "./request";
import { useState } from "react";
import Navbar from "./Navbar";
import { useAuth } from "./AuthContext";

const CreateEvent = () => {
    const [eventName, setEventName] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [eventStart, setEventStart] = useState('');
    const [eventEnd, setEventEnd] = useState('');
    const { currentUser } = useAuth();

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (!eventName || !eventStart || !eventEnd) return;
        console.log(currentUser.id);
        const newEvent = {
            eventName, description: description !== '' ? description.trim() : null,
            location: location !== '' ? location.trim() : null,
            eventStart, eventEnd, userId: currentUser.id
        };
        const response = await request("POST", "http://localhost:5183/api/events", newEvent);
        console.log(response);
        // console.log(newEvent);


    }



    return (
        <div>
            <Navbar />
            <h2>Add New Event</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Event Name"
                    required
                    value={eventName}
                    onChange={(e) => setEventName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Location"
                    value={location}
                    onChange={(e) => setLocation(e.target.value)}
                />

                <label htmlFor="event-start">Event Start: </label>
                <input value={eventStart} onChange={(e) => setEventStart(e.target.value)}
                    type="datetime-local" id="event-start" name="event-start"></input>
                <label htmlFor="event-end">Event End: </label>
                <input required value={eventEnd} onChange={(e) => setEventEnd(e.target.value)}
                    type="datetime-local" id="event-end" name="event-end"></input>
                <button>Create</button>
            </form>
            {/* {error && <p>Create failed</p>} */}
        </div>
    );
}

export default CreateEvent;