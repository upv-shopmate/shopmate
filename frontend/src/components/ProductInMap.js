/* eslint react/prop-types: 0 */
import '../assets/css/ProductInMap.css';
import React from 'react';
import { Store } from '../utils/Store.js'

class ProductInMap extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
    };
    this.waterDrop = React.createRef();
  }

  componentDidMount() {
      let coor = this.getCoordinates();
      let left = coor[0] + "px";
      let top = coor[1] + "px";
      this.waterDrop.current.style.left = left;
      this.waterDrop.current.style.top = top;
  }

  getCoordinates() {
      let pos = this.props.pos;
      let left = (pos[0] * 10) - 40;
      let top = (pos[1] * 10) - 80;
      return [left, top];
  }
  
  render() {
    return (
        <div className="product-in-map">
            <div 
                className="product-in-map-wrapper"
                ref={this.waterDrop}
            >
                <img
                    src={this.props.image}
                    className="product-in-map-image"
                >
                </img>
                <div className="product-in-map-drop"></div>
            </div>
        </div>
    );
  }
}

export default ProductInMap;