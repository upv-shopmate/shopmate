const request = require('axios');
import {dataBaseURL} from '../components/App.js';

export async function requestCouponDataBase() {
  let contents = 0;
  await request({
    'url': dataBaseURL + '/api/Coupon/',
    'rejectUnauthorized': false,
  })
      .then(function(response) {
        {
          contents = response.data;
        }
      });
  return contents;
}
