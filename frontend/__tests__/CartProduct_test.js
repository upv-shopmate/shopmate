import React from 'react';
import renderer from 'react-test-renderer';
import { shallow, configure, } from 'enzyme'
import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

import CartProduct from '../src/components/CartProduct'

test('unitary price, quantity == 1', () => {
    const entry = {
        item: {
            pictures: [""],
            name: "foo",
            barcode: "xdfedfwdf",
            priceWithVat: 1.4,
        },
        quantity: 1
    }
    const component = shallow(
        <CartProduct entry={entry} key={entry.item.barcode}></CartProduct>
    ).dive();
    let priceDiv = component.find(".entrie-unit-price")
    expect(priceDiv.text()).toEqual("")
});

test('unitary price, quantity > 1', () => {
    const entry = {
        item: {
            pictures: [""],
            name: "foo",
            barcode: "xdfedfwdf",
            priceWithVat: 1.4,
        },
        quantity: 2
    }
    const component = shallow(
        <CartProduct entry={entry} key={entry.item.barcode}></CartProduct>
    ).dive();
    let priceDiv = component.find(".entrie-unit-price")
    expect(priceDiv.text()).toEqual("1.40unitaryText") //40unitaryText  because of translation
});

test('price multiplied correctly', async () => {
    let price = 2;
    let quantity = 2;
    const entry = {
        item: {
            pictures: [],
            name: "foo",
            barcode: "asdadasf",
            priceWithVat: price,
        },
        quantity: quantity,
        totalPrice: price * quantity,
    }
    const component = shallow(
        <CartProduct entry={entry} key={entry.item.barcode}></CartProduct>
    ).dive();
    let unitaryPrice = component.find(".entrie-unit-price").text();
    let totalPrice = component.find(".entrie-price").text();
    expect(totalPrice).toEqual((price * quantity).toFixed(2) + " €");
    expect(unitaryPrice).toEqual(price.toFixed(2) + "unitaryText");
});