/* eslint react/prop-types: 0 */
import '../assets/css/ProductDetails.css';
import closeButton from '../assets/images/close_button.png';
import mapButton from '../assets/images/map_button.png';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';
import {withTranslation} from 'react-i18next';

class ProductDetails extends Component {
  constructor(props) {
    super(props);
    this.viewDetails = this.viewDetails.bind(this);
    this.closeDetailsPanel = this.closeDetailsPanel.bind(this);
  }

  roundUp(num, precision) {
    precision = Math.pow(10, precision);
    return (Math.ceil(num * precision) / precision).toFixed(2);
  }

  getProductName() {
    const name = this.props.product.name;
    return capitalize(name);
  }

  getProductImage() {
    const image = this.props.product.pictures[0];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  getProductBrand() {
    const aux = this.props.product.brands;
    if (aux !== [] && aux[0] !== undefined) {
      return 'de ' + aux[0].name;
    }
    return '';
  }

  getProductPrice() {
    return this.roundUp(this.props.product.priceWithVat, 2) + '€';
  }

  closeDetailsPanel() {
    this.props.closePanel();
  }

  getProductWeight() {
    const aux = this.props.product.weight;
    if (aux !== null) {
      return aux + 'g';
    } else {
      return 'N/A';
    }
  }

  getProductOriginCountry() {
    const aux = this.props.product.originCountry;
    if (aux !== null) {
      return aux;
    } else {
      return 'N/A';
    }
  }

  getProductVolume() {
    const aux = this.props.product.volume;
    if (aux !== null) {
      return aux;
    } else {
      return 'N/A';
    }
  }

  isProductStocked() {
    const {t} = this.props;
    const aux = this.props.product.availableStock;
    if (aux > 0) {
      return t('yes') + ' (' + aux + ')';
    } else {
      return t('no');
    }
  }

  isProductEdible() {
    const {t} = this.props;
    if (this.props.product.edible) {
      return t('yes');
    } else {
      return t('no');
    }
  }

  getMoreInfo() {
    const {t} = this.props;
    return (
      <React.Fragment>
        <div className="details-text-line">
          <span className="details-text-header">{t('weight')}</span>
          <span className="details-text">{this.getProductWeight()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">{t('volume')}</span>
          <span className="details-text">{this.getProductVolume()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">{t('originCountry')}</span>
          <span className="details-text">{this.getProductOriginCountry()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">{t('stocked')}</span>
          <span className="details-text">{this.isProductStocked()}</span>
        </div>
      </React.Fragment>
    );
  }

  viewDetails() {
    if (this.props.product !== null) {
      return (
        <div className="details">
          <img className="details-image" src={this.getProductImage()}></img>
          <div className="details-title">
            <div className="details-name">{this.getProductName()}</div>
            <div className="details-brand">{this.getProductBrand()}</div>
            <div className="details-price">{this.getProductPrice()}</div>
          </div>
          <div className="details-box">
            <div className="details-close-button">
              <img className="details-close-button-image"
                src={closeButton}
                onClick={this.closeDetailsPanel}
              >
              </img>
            </div>
            <div className="details-info">{this.getMoreInfo()}</div>
            <div className="details-map-button">
              <img className="details-map-button-image" src={mapButton}>
              </img>
            </div>
          </div>
        </div>
      );
    } else {
      return <div></div>;
    }
  }

  render() {
    return this.viewDetails();
  }
}

export default withTranslation()(ProductDetails);
