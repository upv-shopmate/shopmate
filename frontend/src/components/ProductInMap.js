/* eslint react/prop-types: 0 */
import '../assets/css/ProductInMap.css';
import React from 'react';
import {Bounce} from 'react-reveal';

class ProductInMap extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
    };
    this.waterDrop = React.createRef();
  }

  componentDidMount() {
    const coor = this.getCoordinates();
    const left = coor[0] + 'px';
    const top = coor[1] + 'px';
    this.waterDrop.current.style.left = left;
    this.waterDrop.current.style.top = top;
  }

  getCoordinates() {
    const pos = this.props.pos;
    let left = (pos[0] * 10) - 25;
    if (left < 0) left = 0;
    let top = (pos[1] * 10) - 50;
    if (top < 40) {
      const newTriangle = document.createElement('div');
      newTriangle.setAttribute('id', 'product-in-map-drop-top');
      this.waterDrop.current.children[1].remove();
      this.waterDrop.current.insertBefore(
          newTriangle,
          this.waterDrop.current.children[0],

      );
      top = 10;
    }
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
            <div id="product-in-map-drop"></div>
          </Bounce>
        </div>
      </div>
    );
  }
}

export default ProductInMap;
