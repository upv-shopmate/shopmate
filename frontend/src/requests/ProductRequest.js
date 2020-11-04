const request = require('axios');
import {dataBaseURL} from '../components/App.js';

export async function requestCatalog() {
  let products = 0;
  await request({
    'url': dataBaseURL + '/api/Products?itemsPerPage=100',
    'rejectUnauthorized': false,
  })
      .then(function(response) {
        {
          products = response.data.items;
        }
      });
  return products;
}


