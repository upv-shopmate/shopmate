var request = require('axios');
import { dataBaseURL } from '../components/App.js';

export async function requestSearchDataBase(input) {
    let products = 0;
    await request.get({ url: dataBaseURL + "/api/Products/search?query=" + input + '&itemsPerPage=100', "rejectUnauthorized": false })
        .then(function (body) {
            {
                products = JSON.parse(body)

            }
        })
    return products;
}