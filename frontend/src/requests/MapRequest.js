const request = require('axios');
import {dataBaseURL} from '../components/App.js';

export async function requestMap() {
  let map = null;
  await request({
    'url': dataBaseURL + '/api/Stores/1/map',
    'rejectUnauthorized': false,
    'accept': 'application/json'
  })
      .then(function(response) {
        {
          map = response.data
        }
      });
  return map;
}


