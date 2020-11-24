const request = require('axios');
import {dataBaseURL} from '../components/App.js';

// query example: https://localhost:5001/api/User/authorize
export async function userAuthRequest(username, password) {
  let accessToken;
  let status;
  const query = '/api/User/authorize';

  await request.post(
      dataBaseURL + query,
      {'username': username, 'password': password})
      .then(function(response) {
        {
          status = response.status;
          if (status == '200') {
            accessToken = response.data.accessToken;
          }
        }
      }).catch(function(error) {
        try {
          status = error.response.status;
        } catch (e) {
          status = 'ConnectionError';
        }
      });

  return {'status': status, 'accessToken': accessToken};
}

export async function userInfoRequest(accessToken) {
  let status;
  let data;
  const query = '/api/User';

  await request(
      dataBaseURL + query,
      {
        headers: {
          'Authorization': 'Bearer ' + accessToken,
        },
      })
      .then(function(response) {
        {
          status = response.status;
          if (status == '200') {
            data = response.data;
          }
        }
      }).catch((error) => {
        status = error.status;
      });
  return {'status': status, 'data': data};
}

export async function userListsRequest(accessToken) {
  let status;
  let data;
  const query = '/api/User/lists';
 
  await request(
    dataBaseURL + query,
    {
      headers: {
        'Authorization': 'Bearer ' + accessToken,
      },
    })
    .then(function(response) {
      {
        status = response.status;
        if (status == '200') {
          data = response.data;
        }
      }
    }).catch((error) => {
      status = error.status;
    });
return {'status': status, 'data': data};


}