import { useState } from "react";
import Navbar from "./Navbar";
import request from "./request";
import { useNavigate } from "react-router-dom";


const CreateUser = () => {
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('User')
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newUserOrAdmin = { name, surname, email, password, role };
        const response = await request("POST", "http://localhost:5183/api/users", newUserOrAdmin);
        console.log(response.data);
        setError(response.error);
        if (!response.error) navigate("/");
    }

    return (
        <div>
            <Navbar />
            <h2>Add a New User</h2>
            <form onSubmit={handleSubmit}>
                <input
                    type="text"
                    placeholder="Name"
                    required
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Surname"
                    required
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Email"
                    required
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Password"
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <label>Role: </label>
                <select
                    value={role}
                    onChange={(e) => setRole(e.target.value)}>
                    <option value="User">User</option>
                    <option value="Admin">Admin</option>
                </select>
                <button>Create</button>
            </form>
            {error && <p>Create failed</p>}
        </div>
    );
}

export default CreateUser;