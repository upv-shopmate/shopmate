const request = require('axios');
import { dataBaseURL } from '../components/App.js';

export async function requestCartContentDataBase() {
  let contents = 0;
  await request({
    'url': dataBaseURL + '/api/Cart/list'
  })
    .then(function (response) {
      {
        contents = response.data;
      }
    });
  return contents;
}
