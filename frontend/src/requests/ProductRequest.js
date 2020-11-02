var request = require('axios');
import { dataBaseURL } from '../components/App.js';

export async function requestDataBase() {
    let products = 0;
    await request({ url: dataBaseURL + '/api/Products?itemsPerPage=4000', "rejectUnauthorized": false })
        .then(function (response) {
            {
                products = response.data.items;
            }
        })
    return products;
}


