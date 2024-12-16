import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Navbar from "./Navbar";
import request from "./request";

const Users = () => {
    const [users, setUsers] = useState(null);
    const [error, setError] = useState(null);


    useEffect(() => {
        async function getSetData() {
            const { data, error } = await request("GET", "http://localhost:5183/api/users", null);
            // console.log(data);
            setUsers(data);
            setError(error);
        }
        getSetData();
    }, []);




    return (
        <div>
            <Navbar />
            {users && users.map((user) => (
                <div key={user.userId}>
                    <Link to={`/user/${user.userId}`}>
                        <h1>{user.name} {user.surname}</h1>
                    </Link>
                </div>
            )
            )}
            {error && <p>An error occurred</p>}
            {error && <p>{error}</p>}
        </div>);
}



export default Users;