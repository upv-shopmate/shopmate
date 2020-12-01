import React from 'react';
import renderer from 'react-preset-renderer';

import {CartProduct} from '../src/components/CartProduct'

test('unitary price', () => {
    const component = renderer.create(<CartProduct/>)
});