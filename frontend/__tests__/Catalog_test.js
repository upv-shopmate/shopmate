import React from 'react';
import { mount, shallow, configure } from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';

import Catalog from '../src/components/Catalog';

configure({ adapter: new Adapter() });

let data = {};

test('item info panel opens', () => {
    const wrapper = mount(
        <Catalog catalog={data} page={1}/>
    );

    const productsPanel = wrapper.childAt(0).children();
    console.log(productsPanel);
});