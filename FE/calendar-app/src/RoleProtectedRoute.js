import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthContext";


const RoleProtectedRoute = ({ children, roles }) => {
    const { currentUser } = useAuth();


    if (!currentUser?.isAuthenticated || currentUser === null) {
        console.log(currentUser);
        return <Navigate to="/login" replace />
    }

    if (!roles.some((role) => currentUser.role?.includes(role))) {
        return <p>Unauthorized</p>;
    }

    return children;
}

export default RoleProtectedRoute;