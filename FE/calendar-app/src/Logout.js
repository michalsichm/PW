import { useNavigate } from "react-router-dom";
import { useAuth } from "./AuthContext";


const Logout = () => {
    const { logout } = useAuth();
    const navigate = useNavigate();



    const handleLogout = async () => {
        logout()
        navigate("/login", { replace: true });
    }

    return (
        <button onClick={() => handleLogout()}>Logout</button>
    );

}



export default Logout;