import React from 'react';
import { mount, shallow, configure, render } from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';

import Catalog from '../src/components/Catalog';

configure({ adapter: new Adapter() });


test('item info panel opens', async () => {
    let data = [
        {
            pictures: [],
            name: "foo",
            barcode: "234234",
            priceWithVat: 1.4,
            brands: ["xd"]
        },
    ];
    const wrapper = mount(
        <Catalog catalog={data} page={1}/>
    );

    const productsPanel = wrapper.find(".products");
    expect(wrapper.state().height).toEqual("100%");
    await productsPanel.childAt(0).find("button").simulate("click");
    expect(wrapper.state().height).toEqual("60%");
    
});

describe('items appear distributed in pages', () => {
    
    test('Backend sends 20 items or less', () => {
        let itemsPerCatalog = 20
        let testCatalog = generateCatalog(itemsPerCatalog);
        const wrapper = mount(
            <Catalog catalog={testCatalog} page={1} />
        );
        let productsPanel = wrapper.find(".products").find(".product");
        expect(productsPanel.length).toBeLessThanOrEqual(20);
    });

    test('Backend sends 21 items or more', () => {
        let itemsPerCatalog = 21
        let testCatalog = generateCatalog(itemsPerCatalog);
        const wrapper = mount(
            <Catalog catalog={testCatalog} page={1} />
        );
        let productsPanel = wrapper.find(".products").find(".product");
        expect(productsPanel.length).toBeLessThanOrEqual(20);
    });

});

function generateCatalog(iterations) {
    let catalog = [];
    for (let i = 0; i < iterations; i++) {
        catalog.push({
            pictures: [],
            name: "foo",
            barcode: i.toString(),
            priceWithVat: 1.4,
            brands: ["xd"]
        });
    }
    return catalog;
}