import { createContext, useContext, useState } from "react";
import request from "./request";


const AuthContext = createContext();

export default function AuthProvider({ children }) {

    const [currentUser, setCurrentUser] = useState(setUser());



    function setUser() {
        const token = localStorage.getItem("token");
        if (!token) return null;
        const payload = JSON.parse(atob(token.split('.')[1]));
        return { id: payload.id, role: payload.role, isAuthenticated: true };
    }

    const login = async (email, password) => {
        const loginRequest = { email, password };
        const response = await request("POST", "http://localhost:5183/api/auth/login", loginRequest, false);
        if (!response.error) {
            localStorage.setItem("token", response.data.token);
            setCurrentUser(setUser());
        }
        return response;
    }

    const register = async (registerRequest) => {
        const response = await request("POST", "http://localhost:5183/api/auth/register", registerRequest, false);
        if (!response.error) {
            localStorage.setItem("token", response.data.token);
            setCurrentUser(setUser());
        }
        return response;
    }


    const logout = () => {
        setUser(null);
        localStorage.removeItem("token");
    }

    return (
        <AuthContext.Provider value={{ currentUser, login, logout, register }}>
            {children}
        </AuthContext.Provider>
    );
}

export const useAuth = () => useContext(AuthContext);
