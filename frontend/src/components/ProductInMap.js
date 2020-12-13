/* eslint react/prop-types: 0 */
import '../assets/css/ProductInMap.css';
import React from 'react';
import { Store } from '../utils/Store.js'
import {Bounce} from 'react-reveal'

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
      let left = (pos[0] * 10) - 25;
      if(left < 0) left = 0;
      let top = (pos[1] * 10) - 50;
      return [left, top];
  }
  
  render() {
    return (
        <div className="product-in-map">
            <div 
                className="product-in-map-wrapper"
                ref={this.waterDrop}
            >
                <Bounce top>
                    <img
                        src={this.props.image}
                        className="product-in-map-image"
                    >
                    </img>
                    <div className="product-in-map-drop"></div>
                </Bounce>
            </div>
        </div>
    );
  }
}

export default ProductInMap;