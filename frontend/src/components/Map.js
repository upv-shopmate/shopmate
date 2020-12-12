/* eslint react/prop-types: 0 */
import '../assets/css/Map.css';
import React from 'react';
import { Store } from '../utils/Store.js'
import ProductInMap from './ProductInMap'

// 853x566
// 85x56
// 10x10
class Map extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'map': props.map,
    };
    const store = Store().getInstance()
    store.subscribe(() => this.forceUpdate());
  }
  
  showProduct() {
    const store = Store().getInstance()
    if(store.getState().productInMap == null) {
      return null;
    } else {
      const product = store.getState().productInMap;
      const img = product.pictures[0];
      const pos = [100, 300];
      return <ProductInMap image={img} pos={pos}/>
    }
  }

  render() {
    return (
      <div className="map">
        <div className="map-layout">
          {this.state.map}
        </div>
        <this.showProduct></this.showProduct>
      </div>
    );
  }
}

export default Map;
