const request = require('axios');
import { dataBaseURL } from '../components/App.js';

// query example: /Products/search?query=queso&page=0&itemsPerPage=10
export async function requestSearchDataBase(input, page) {
  let products = 0;
  const query = '/api/Products/search?query=';
  await request({
    'url': dataBaseURL + query + input + '&page=' + page + '&itemsPerPage=20',
    'rejectUnauthorized': false,
  })
    .then(function (response) {
      {
        products = response.data;
      }
    });
  return products;
}
