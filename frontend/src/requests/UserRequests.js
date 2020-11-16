const request = require('axios');
import { dataBaseURL } from '../components/App.js';

// query example: https://localhost:5001/api/User/authorize
export async function userAuthRequest(username, password) {
    let accesToken;
    let status;
    const query = '/api/User/authorize';

    await request.post(
        dataBaseURL + query,
        { "username": username, "password": password })
        .then(function (response) {
            {
                status = response.status;
                if (status == '200') {
                    accesToken = response.data.accessToken;
                }
            }
        }).catch(error => {
            status = error.status;
        });

    return { "status": status, "accesToken": accesToken };
}

export async function userInfoRequest(accesToken) {
    let status;
    let data;
    const query = '/api/User';

    await request(
        dataBaseURL + query,
        {
            headers: {
                'Authorization': 'Bearer ' + accesToken
            }
        })
        .then(function (response) {
            {
                status = response.status;
                if (status == '200') {
                    data = response.data;
                }
            }
        }).catch(error => {
            status = error.status;
        });
    return { "status": status, "data": data };
}
