import { useState } from "react";
import { useAuth } from "./AuthContext";
import { useEffect } from "react";
import request from "./request";
import Navbar from "./Navbar";

const UserProfile = () => {
    const { currentUser } = useAuth();
    const [user, setUser] = useState();
    const [oldPassword, setOldPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");

    useEffect(() => {
        async function getSetData() {
            const response = await request("GET", `http://localhost:5183/api/users/${currentUser.id}`, null, true);
            setUser(response.data);
        }
        getSetData();
    }, []);


    const handleSubmit = async (e) => {
        e.preventDefault();
        let u = oldPassword && newPassword ? { ...user, oldPassword, newPassword } : user
        // if (oldPassword && newPassword) {
        await request("PUT", `http://localhost:5183/api/users`, u, true);
        // }
        // else {
        //     await request("PUT", `http://localhost:5183/api/users`, { ...user, oldPassword, newPassword }, true);
        // }
        console.log(u);
    }


    const updateName = (value) => {
        setUser(prevUser => ({ ...prevUser, name: value }));
    };

    const updateSurname = (value) => {
        setUser(prevUser => ({ ...prevUser, surname: value }));
    };

    return (<div>
        <Navbar />
        {user && <div>
            <form onSubmit={handleSubmit}>
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
                <button>Update</button>
            </form>
            <h1>Change Password</h1>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Old Password"
                    required
                    onChange={(e) => setOldPassword(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="New Password"
                    required
                    onChange={(e) => setNewPassword(e.target.value)}
                />
                <button>Change</button>
            </form>
        </div>}
    </div>);
}

export default UserProfile;