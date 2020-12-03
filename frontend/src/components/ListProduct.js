/* eslint react/prop-types: 0 */
import '../assets/css/ListProduct.css';
import React from 'react';
import checkImage from '../assets/images/check.png';
import {withTranslation} from 'react-i18next';

class ListProduct extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      checked: false,
    };
    this.imageRef = React.createRef();
    this.checkRef = React.createRef();
  }

  handleProductClick() {
    if (!this.state.checked) {
      this.checkRef.current.classList.add('list-product-display-check');
      this.imageRef.current.classList.add('list-product-opacity-low');
    } else {
      this.checkRef.current.classList.remove('list-product-display-check');
      this.imageRef.current.classList.remove('list-product-opacity-low');
    }
    this.setState({
      checked: !this.state.checked,
    });
  }

  render() {
    const {t} = this.props;
    return (
      <div className="list-product-wrapper"
        onClick={() => this.handleProductClick()}
      >
        <div className="list-product-image-wrapper">
          <img src={this.props.image} ref={this.imageRef}></img>
          <img
            src={checkImage}
            ref={this.checkRef}
            className="list-product-image-check"></img>
        </div>
        <div className="list-product-info">
          <div className="list-product-name">{this.props.name}</div>
          <div className="list-product-quantity">
            {t('quantity')} {this.props.quantity}
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation()(ListProduct);
