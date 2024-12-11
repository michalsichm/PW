import { useEffect, useState } from "react";
import Navbar from "./Navbar";
import request from "./request";

const Users = () => {
    const [users, setUsers] = useState(null);
    const [error, setError] = useState(null);


    useEffect(() => {
        async function getSetData() {
            const { data, error } = await request("GET", "http://localhost:5183/api/users");
            // console.log(data);
            setUsers(data);
            setError(error);
        }
        getSetData();
    }, []);



    const handleDeleteUser = async (userId) => {
        await request("DELETE", `http://localhost:5183/api/users/${userId}`)
        setUsers(users.filter((user) => user.userId !== userId));
    }


    return (
        <div>
            <Navbar />
            {users && users.map((user) => (
                <div key={user.userId}>
                    <h1>{user.name}</h1>
                    <h1>{user.surname}</h1>
                    <p>Role: {user.role}</p>
                    <button onClick={() => { handleDeleteUser(user.userId) }}>Delete</button>
                </div>
            )
            )}
            {error && <p>An error occurred</p>}
            {/* {error && <p>{error}</p>} */}
        </div>);
}


// const UsersList = ({ usersList }) => {
//     return (<div>
//         {usersList && usersList.map((user) => (
//             <div key={user.userId}>
//                 <h1>{user.name}</h1>
//                 {/* <h1>{user.surname}</h1> */}
//                 <button onClick={() => { handleDeleteUser(user.userId) }}>Delete</button>
//             </div>
//         )
//         )}
//     </div>);

// }


export default Users;