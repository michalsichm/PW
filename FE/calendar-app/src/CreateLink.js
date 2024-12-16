import { useAuth } from "./AuthContext";
import request from "./request";

const CreateLink = () => {
    const { currentUser } = useAuth();


    const getLink = async () => {
        const response = await request("GET", `http://localhost:5183/api/shareevents/${currentUser.id}`);
        const link = 'http://localhost:3000/share' + response.data.link; // :)
        if (!response.error) {
            alert(link);
        }
    }

    return (
        <button onClick={() => getLink()}>Share my events</button>
    );

}



export default CreateLink;