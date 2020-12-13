const request = require('axios');
import { dataBaseURL } from '../components/App.js';

export async function requestCatalog(page) {
  let products = 0;
  await request({
    'url': dataBaseURL + '/api/Products?page=' + page + '&itemsPerPage=20',
    'rejectUnauthorized': false,
  })
    .then(function (response) {
      {
        products = response.data.items;
      }
    });
  return products;
}

//https://localhost:5001/api/Products?page=1&itemsPerPage=20&category=1
export async function requestCatalogByCategory(id, page) {
  let products = 0;
  await request({
    'url': dataBaseURL + '/api/Products?page=' + page + '&itemsPerPage=20&category=' + id,
    'rejectUnauthorized': false,
  })
    .then(function (response) {
      {
        (id);
        products = response.data.items;
      }
    });
  return products;
}


