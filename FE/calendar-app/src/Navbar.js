import { Link } from "react-router-dom";
import { useAuth } from "./AuthContext";
import CreateLink from "./CreateLink";



const Navbar = () => {
    const { currentUser } = useAuth();


    if (currentUser.role === "User") {
        return (
            <div>
                <Link to="/">Home</Link>
                <Link to="/create-event">Create Event</Link>
                <CreateLink />
            </div>
        );
    }

    return (
        <div>
            <Link to="/">Home</Link>
            <Link to="/create">Create</Link>
        </div>
    );
}

export default Navbar;