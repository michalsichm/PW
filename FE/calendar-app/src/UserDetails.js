import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import request from "./request";
import Navbar from "./Navbar";
import Events from "./Events";

const getUser = (id) => {
    return request("GET", `http://localhost:5183/api/users/${id}`, null, true);
}

const getUserEvents = (id) => {
    return request("GET", `http://localhost:5183/api/users/${id}/events`, null, true);

}



const UserDetails = () => {
    const { id } = useParams();
    const [user, setUser] = useState(undefined);
    const [userEvents, setUserEvents] = useState(null);


    useEffect(() => {
        const loadData = async function () {
            const [userData, eventsData] = await Promise.all([getUser(id), getUserEvents(id)]);
            // check for error
            // console.log(eventsData.data);
            setUser(userData.data);
            setUserEvents(eventsData.data);
        }
        loadData();
    }, [id])

    return (
        <div>
            <Navbar />
            {user &&
                <div key={user.userId}>
                    <h1>{user.name}</h1>
                    <h1>{user.surname}</h1>
                </div>
            }
            <Events events={userEvents}/>
            {user === null && <p>This user doesn't exist</p>}
        </div>
    );
}
export default UserDetails;