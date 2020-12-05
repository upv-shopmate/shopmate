import React from 'react';
import { mount, shallow, configure, render } from 'enzyme';
import Adapter from 'enzyme-adapter-react-16';

import SearchPanel from '../src/components/SearchPanel';

configure({ adapter: new Adapter() });




describe('Searching items in Search Panel', () => {
    
    test('Search results equals 0',  () => {
        let data = [];
       
    
        const panel = mount(
            <SearchPanel
                completedSearch= {true}
                results= {data}
            />
        );
    
        const number = panel.find('.searcher-title');
        expect(number.text()).toEqual('searchNoResults');    
    
    });

    test('Search results equals more than 0', () => {
        let data = [ {
            pictures: [],
            name: "foo",
            barcode: "234234",
            priceWithVat: 1.4,
            brands: ["xd"],
            categories: [
                {
                  "id": 0,
                  "name": "string"
                }
              ],
        }, {
            pictures: [],
            name: "foo",
            barcode: "123456",
            priceWithVat: 1.4,
            brands: ["xd"],
            categories: [
                {
                  "id": 0,
                  "name": "string"
                }
              ],
        }
    ];

        const panel = mount(
            <SearchPanel
                completedSearch= {true}
                results= {data}
            />
        );
    
        const number = panel.find('.searcher-results').find('.result');
        expect(number.length).toBeGreaterThanOrEqual(1);
    });

});
