import React from 'react';
import renderer from 'react-test-renderer';
import { shallow, configure, } from 'enzyme'
import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

import Cart from '../src/components/Cart'

test('Price shows correctly', () => {
    const cart = {
        "id": 0,
        "name": "",
        "subtotalPrice": 4.94,
        "totalPrice": 5.9774,
        "modifierBreakdowns": [
            {
                "modifier": {
                    "code": "Vat",
                    "description": "",
                    "value": 0.21,
                    "kind": "Multiplicative"
                },
                "applicableBase": 4.94,
                "totalDelta": 1.0374
            }
        ],
        "entries": [
            {
                "quantity": 3,
                "totalPrice": 5.6628,
                "item": {
                    "barcode": "08410100025155",
                    "name": "Nesquik",
                    "weight": 400,
                    "volume": null,
                    "units": 1,
                    "originCountry": null,
                    "edible": true,
                    "price": 1.56,
                    "pictures": "",
                    "availableStock": 995,
                    "timesSold": 644,
                    "brands": [
                        {
                            "id": 636,
                            "name": "Nestlé",
                            "aliases": [],
                            "logo": null
                        }
                    ],
                    "categories": [
                        {
                            "id": 10923,
                            "name": "Cocoa and chocolate powders"
                        },
                        {
                            "id": 10924,
                            "name": "Instant beverages"
                        },
                        {
                            "id": 10925,
                            "name": "Chocolate powders"
                        },
                        {
                            "id": 10926,
                            "name": "Sweetened beverages"
                        },
                        {
                            "id": 10952,
                            "name": "Breakfasts"
                        },
                        {
                            "id": 10954,
                            "name": "Beverages"
                        }
                    ],
                    "labels": [],
                    "priceModifiers": [
                        {
                            "code": "Vat",
                            "description": "",
                            "value": 0.21,
                            "kind": "Multiplicative"
                        }
                    ],
                    "vendors": [
                        {
                            "id": 1,
                            "name": "Mercadona",
                            "currency": "EUR"
                        }
                    ],
                    "modifiedPrice": 1.8876,
                    "priceWithVat": 1.8876
                }
            },
            {
                "quantity": 1,
                "totalPrice": 0.3146,
                "item": {
                    "barcode": "08426967022510",
                    "name": "Galletas",
                    "weight": null,
                    "volume": null,
                    "units": 1,
                    "originCountry": null,
                    "edible": true,
                    "price": 0.26,
                    "pictures": "",
                    "availableStock": 52,
                    "timesSold": 98,
                    "brands": [
                        {
                            "id": 800,
                            "name": "Hacendado",
                            "aliases": [],
                            "logo": null
                        }
                    ],
                    "categories": [
                        {
                            "id": 8717,
                            "name": "Snacks"
                        },
                        {
                            "id": 8718,
                            "name": "Sweet snacks"
                        },
                        {
                            "id": 8719,
                            "name": "Biscuits and cakes"
                        },
                        {
                            "id": 8753,
                            "name": "Biscuits"
                        }
                    ],
                    "labels": [],
                    "priceModifiers": [
                        {
                            "code": "Vat",
                            "description": "",
                            "value": 0.21,
                            "kind": "Multiplicative"
                        }
                    ],
                    "vendors": [
                        {
                            "id": 1,
                            "name": "Mercadona",
                            "currency": "EUR"
                        }
                    ],
                    "modifiedPrice": 0.3146,
                    "priceWithVat": 0.3146
                }
            }
        ]

    }
    const component = shallow(
        <Cart />
    ).dive();
    component.setState({
        cartContent: cart,
    });
    let priceDiv = component.find(".price")
    expect(priceDiv.text()).toEqual("5.98 € ")
});