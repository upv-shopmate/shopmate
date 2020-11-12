/* eslint react/prop-types: 0 */
import '../assets/css/Product.css';
import '../assets/css/fonts.css';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';

class Product extends Component {
  constructor(props) {
    super(props);
    this.state = {
      product: this.props.productData,
    };
    this.setProduct = this.setProduct.bind(this);
  }

  roundUp(num, precision) {
    precision = Math.pow(10, precision);
    return (Math.ceil(num * precision) / precision).toFixed(2);
  }

  setProduct() {
    this.props.showProductDetails(this.state.product);
  }

  getBrand() {
    const aux = this.state.product.brands;
    if (aux !== [] && aux[0] !== undefined) {
      return aux[0].name;
    }
    return '';
  }

  getName() {
    const name = this.state.product.name;
    return capitalize(name);
  }

  getPrice() {
    return this.roundUp(this.state.product.priceWithVat, 2);
  }

  getImage() {
    const image = this.state.product.pictures[0];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  render() {
    return (
      <div className="product">
        <button className="product-button" onClick={this.setProduct}>
          <div className="product-view">
            <div className="product-text">
              <div className="product-name">{this.getName()}</div>
              <div className="product-brand">{this.getBrand()}</div>
              <div className="product-price">{this.getPrice()}€</div>
            </div>
            <img className="product-image" src={this.getImage()}></img>
          </div>
        </button>
      </div>
    );
  }
}

export default Product;
