import request from "./request";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";


const Register = () => {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [passwordAgain, setPasswordAgain] = useState("");
    const [passwordMatch, setPasswordMatch] = useState();
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleRegister = async (e) => {
        e.preventDefault();
        if (!passwordMatch) return;
        const registerRequest = { name, surname, email, password };
        const response = await request("POST", "http://localhost:5183/api/auth/register", registerRequest);
        console.log(response.data);
        setError(error);
        if (!response.error) navigate("/", { replace: true });
    };
    useEffect(() => {
        setPasswordMatch(password === passwordAgain);
    }, [password, passwordAgain]);

    return (
        <div>
            <h2>Register</h2>
            <form onSubmit={handleRegister}>

                <input
                    type="text"
                    placeholder="Name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                />
                <input
                    type="text"
                    placeholder="Surname"
                    value={surname}
                    onChange={(e) => setSurname(e.target.value)
                    }
                />
                <input
                    type="text"
                    placeholder="Email"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Enter Password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />
                <input
                    type="password"
                    placeholder="Confirm Password"
                    value={passwordAgain}
                    onChange={(e) => setPasswordAgain(e.target.value)}
                />
                <button type="submit">Register</button>
            </form>
            {error && <p>Error</p>}
            {!passwordMatch && <p>Passwords don't match</p>}
        </div>
    );
}

export default Register;