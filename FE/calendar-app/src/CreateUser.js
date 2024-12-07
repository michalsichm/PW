import { useState } from "react";
import Navbar from "./Navbar";
import request from "./request";



const CreateUser = () => {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('User')
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newUserOrAdmin = { name, surname, email, password, role };
        const {_, error} = await request("POST", "http://localhost:5183/api/users", newUserOrAdmin);
        setError(error);
        // console.log(data);
    }

    return (
        <div>
            <Navbar />
            <h2>Add a New User</h2>
            <form onSubmit={handleSubmit}>
                <label>Name: </label>
                <input
                    type="text"
                    required
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <label>Surname: </label>
                <input
                    type="text"
                    required
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)}
                />
                <label>Email: </label>
                <input
                    type="text"
                    required
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <label>Password: </label>
                <input
                    type="text"
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <label>Role: </label>
                <select
                    value={role}
                    onChange={(e) => setRole(e.target.value)}
                >
                    <option value="User">User</option>
                    <option value="Admin">Admin</option>
                </select>
                <button>Add User</button>
            </form>
            {error && {error}}
        </div>
    );
}

export default CreateUser;