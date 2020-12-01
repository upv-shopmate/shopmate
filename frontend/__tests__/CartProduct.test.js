import React from 'react';
import renderer from 'react-test-renderer';

import CartProduct from '../src/components/CartProduct'

test('unitary price', () => {
    const entry = {
        item: {
            pictures: [""],
            name: "foo",
            barcode: "xdfedfwdf"
        }
    }
    const component = renderer.create(
        <CartProduct entry={entry} key={entry.item.barcode}></CartProduct>
    );
});