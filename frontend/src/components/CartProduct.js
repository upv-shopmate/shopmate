/* eslint react/prop-types: 0 */

import '../assets/css/CartProduct.css';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';

class CartProduct extends Component {
  constructor(props) {
    super(props);
  }

  getEntrieImage() {
    const image = this.props.entry.item.pictures[0];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  getEntrieName() {
    const name = this.props.entry.item.name;
    return capitalize(name);
  }

  getEntrieQuantity() {
    return 'Cantidad: ' + this.props.entry.quantity;
  }

  getEntriePrice() {
    const product = this.props.entry.item;
    return product.priceWithVat.toFixed(2) + ' €';
  }


  render() {
    return (
      <div className="product-entrie">
        <img className="entrie-image" src={this.getEntrieImage()}></img>
        <div className="entrie-info">
          <div className="entrie-topline">
            <div className="entrie-name">{this.getEntrieName()}</div>
          </div>
          <div className="entrie-bottomline">
            <div className="entrie-quantity">
              {this.getEntrieQuantity()}
            </div>
          </div>
        </div>
        <div className="entrie-price">{this.getEntriePrice()}</div>
      </div>
    );
  }
}

export default CartProduct;
