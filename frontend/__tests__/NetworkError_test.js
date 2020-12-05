import React from 'react';
import renderer from 'react-test-renderer';
import { shallow, configure, mount, render, } from 'enzyme'
import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

import ErrorPanel from '../src/components/ErrorPanel';
import App from '../src/components/App';
import {showErrorPanel} from '../src/components/App';


test('Error panel shows correctly', () => {
    
    const component = shallow(
        <ErrorPanel/>
    ).dive();
    
    let errorTitle = component.find(".ep-title");
    expect(errorTitle.text()).toEqual('networkError.title');
    let errorSubtitle = component.find(".ep-subtitle");
    expect(errorSubtitle.text()).toEqual('networkError.message');
   
});


/*test('Error panel shows when there is not connection',  () => {
    
    const component = mount(
        <App
        
        />
    );
    console.log(component)
    expect(component.state().connectionError).toBeFalsy();
    
    const app = mount(
        <App connectionError = {true}/>
    );
    console.log(app);
    expect(app.state().connectionError).toBeTruthy();

});*/
