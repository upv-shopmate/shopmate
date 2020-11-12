const request = require('axios');
import {dataBaseURL} from '../components/App.js';

export async function requestCatalog(page) {
  let products = 0;
  await request({
    'url': dataBaseURL + '/api/Products?page=' + page + '&itemsPerPage=100',
    'rejectUnauthorized': false,
  })
      .then(function(response) {
        {
          products = response.data.items;
        }
      })
      .catch(function(error) {
        console.log(error);
      });
  return products;
}


