import { useState } from "react";




const useRequest = async (requestMethod, url, body = null, token = null) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    try {
        const response = await fetch(url, {
            method: requestMethod,
            headers: { "Content-Type": "application/json" },
            body: body ? JSON.stringify(body) : undefined
        })
        if (!response.ok) {
            const error = await response.json();
            throw new Error(`Request error. Status: ${response.status}. Message: ${error.message}`);
        }
        // console.log(await response.json());
        if (response.status === 204) return;
        setData(await response.json());
        // return await response.json();
    }
    catch (error) {
        console.error(error.message);
        setError(error.message);
        // return null;
    }

    return {data, error};
}



export default useRequest;