import { useAuth } from "./AuthContext";
import Users from "./Users";




const Homepage = () => {
    const { currentUser } = useAuth();


    if (currentUser.role === "Admin") {
        return (<Users />)
    }
    return (<p>Hello User</p>);
}

export default Homepage;