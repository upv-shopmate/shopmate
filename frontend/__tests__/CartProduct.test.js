import React from 'react';
import renderer from '@babel/preset-react';

import {CartProduct} from '../src/components/CartProduct'

test('unitary price', () => {
    const component = renderer.create(<CartProduct/>)
});