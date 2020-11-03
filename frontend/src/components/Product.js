/* eslint react/prop-types: 0 */
import '../assets/css/Product.css';
import '../assets/css/fonts.css';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';

class Product extends Component {
  constructor(props) {
    super(props);
    this.state = {
      product: this.props.productData,
    };
    this.setProduct = this.setProduct.bind(this);
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
    return this.state.product.name.charAt(0).toUpperCase() + this.state.product.name.slice(1);
  }

  getPrice(price) {
    return this.state.product.price.toFixed(2);
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
              <div className="product-price">{this.getPrice(this.state.price)}€</div>
            </div>
            <img className="product-image" src={this.getImage()}></img>
          </div>
        </button>
      </div>
    );
  }
}

export default Product;
