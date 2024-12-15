import { useAuth } from "./AuthContext";
import Events from "./Events";
import Navbar from "./Navbar";
import Users from "./Users";




const Homepage = () => {
    const { currentUser } = useAuth();


    if (currentUser.role === "Admin") {
        return (<Users />)
    }
    return (
        <div>
            <Navbar />
            <Events id={currentUser.id} />
        </div>
    );
}

export default Homepage;