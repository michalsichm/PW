import { Link } from "react-router-dom";
import { useAuth } from "./AuthContext";
import CreateLink from "./CreateLink";
import Logout from "./Logout";



const Navbar = () => {
    const { currentUser } = useAuth();


    if (currentUser.role === "User") {
        return (
            <div>
                <Link to="/profile">Profile</Link>
                <Link to="/">Home</Link>
                <Link to="/create-event">Create Event</Link>
                <CreateLink />
                <Logout />
            </div>
        );
    }

    return (
        <div>
            <Link to="/">Home</Link>
            <Link to="/create">Create</Link>
            <Logout />
        </div>
    );
}

export default Navbar;