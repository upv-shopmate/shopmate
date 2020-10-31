var request = require('axios')

export async function requestDataBase() {
    let products = 0;
    await request({ url: 'https://localhost:5001/api/Products?itemsPerPage=4000', "rejectUnauthorized": false })
        .then(function (body) {
            {
                products = JSON.parse(body)

            }
        })
    return products;
}


