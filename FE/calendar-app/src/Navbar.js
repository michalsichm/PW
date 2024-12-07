import { Link } from "react-router-dom";

const Navbar = () => {
    return (
        <div>
            <Link to="/">Users</Link>
            <Link to="/create">Create</Link>
        </div>
    );
}

export default Navbar;