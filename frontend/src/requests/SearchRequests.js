const request = require('axios');
import {dataBaseURL} from '../components/App.js';

export async function requestSearchDataBase(input, page) {
  let products = 0;
  const query = '/api/Products/search?query=';
  await request({
    'url': dataBaseURL + query + input + '&page=' + page + '&itemsPerPage=10',
    'rejectUnauthorized': false,
  })
      .then(function(response) {
        {
          products = response.data.items;
        }
      });
  return products;
}
