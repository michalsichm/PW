const request = async (requestMethod, url, body = null, token = true) => {
    // const [data, setData] = useState(null);
    // const [error, setError] = useState(null);
    let headers = {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${localStorage.getItem("token")}`

    }


    if (token === false) {
        headers = { "Content-Type": "application/json" }
    }
    let data = null;
    let error = false;
    try {
        const response = await fetch(url, {
            method: requestMethod,
            headers: headers,
            body: body ? JSON.stringify(body) : undefined
        })
        if (!response.ok) {
            if (response.status === 401) {
                localStorage.removeItem("token");
                // redirect to login
            }

            const error = await response.json();
            throw new Error(`Request error. Status: ${response.status}. Message: ${error.message}`);
            // throw new Error(error.message);
        }
        // console.log(await response.json());
        if (response.status === 204) return;
        // setData(await response.json());
        // console.log(response);
        data = await response.json();
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

    return { data, error };
}


export default request;