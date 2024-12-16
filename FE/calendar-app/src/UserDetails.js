import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
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
    const navigate = useNavigate();


    useEffect(() => {
        const loadData = async function () {
            const [userData, eventsData] = await Promise.all([getUser(id), getUserEvents(id)]);
            setUser(userData.data);
            setUserEvents(eventsData.data);
        }
        loadData();
    }, [id])


    const handleDeleteUser = async () => {
        await request("DELETE", `http://localhost:5183/api/users/${id}`)
        navigate('/');
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        const updateUser = { userId: user.userId, name: user.name, surname: user.surname };
        await request("PUT", `http://localhost:5183/api/users`, updateUser, true);
        navigate('/');
    }

    const updateName = (value) => {
        setUser(prevUser => ({ ...prevUser, name: value }));
    };

    const updateSurname = (value) => {
        setUser(prevUser => ({ ...prevUser, surname: value }));
    };



    return (
        <div>
            <Navbar />
            {user && <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Name"
                    required
                    value={user.name}
                    onChange={(e) => updateName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Surname"
                    required
                    value={user.surname}
                    onChange={(e) => updateSurname(e.target.value)}
                />
                <button>Update User</button>
            </form>}
            <button onClick={() => { handleDeleteUser() }}>Delete User</button>
            {userEvents && <Events eventsList={userEvents} />}
            {user === null && <p>This user doesn't exist</p>}
        </div>
    );
}
export default UserDetails;