// import { useState } from "react";




const request = async (requestMethod, url, body = null, token = null) => {
    // const [data, setData] = useState(null);
    // const [error, setError] = useState(null);
    let data = null;
    let error = false;
    try {
        const response = await fetch(url, {
            method: requestMethod,
            headers: { "Content-Type": "application/json" },
            body: body ? JSON.stringify(body) : undefined
        })
        if (!response.ok) {
            const error = await response.json();
            throw new Error(`Request error. Status: ${response.status}. Message: ${error.message}`);
            // throw new Error(error.message);
        }
        // console.log(await response.json());
        if (response.status === 204) return;
        // setData(await response.json());
        data = await response.json()
        // return { data, error };
        // return await response.json();
    }
    catch (e) {
        // console.log(e);
        console.error(e.message);
        error = true;
        // return null;
        // setError(error.message);
        // error = true;
        // return { data, error };
    }

    return {data, error};
}



export default request;