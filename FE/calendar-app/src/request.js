const request = async (requestMethod, url, body = null, token = true) => {
    // make this prettier
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
        }
        if (response.status === 204) return;
        data = await response.json();
    }
    catch (e) {
        console.error(e.message);
        error = true;
    }

    return { data, error };
}


export default request;