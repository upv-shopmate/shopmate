const request = require('axios');
import { dataBaseURL } from '../components/App.js';

//https://localhost:5001/api/Categories
export async function requestAllCategories() {
    let categories = 0;
    const query = '/api/Categories';
    await request({
        'url': dataBaseURL + query,
        'rejectUnauthorized': false,
    })
        .then(function (response) {
            {
                categories = response.data;
            }
        });
    return categories;
}