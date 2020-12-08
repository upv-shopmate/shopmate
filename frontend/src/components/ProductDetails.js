/* eslint react/prop-types: 0 */
import '../assets/css/ProductDetails.css';
import closeButton from '../assets/images/close_24px.png';
import mapButton from '../assets/images/map_button.png';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, { Component } from 'react';
import { capitalize } from '../utils/Utils';
import { withTranslation } from 'react-i18next';
import { roundUp } from '../utils/Utils';

class ProductDetails extends Component {
  constructor(props) {
    super(props);
    this.viewDetails = this.viewDetails.bind(this);
    this.closeDetailsPanel = this.closeDetailsPanel.bind(this);
    this.goToLeftImage = this.goToLeftImage.bind(this);
    this.goToRightImage = this.goToRightImage.bind(this);
    this.zoomImage = this.zoomImage.bind(this);
    this.state = {
      imagePage: 1,
    }
  }

  getProductName() {
    const name = this.props.product.name;
    return capitalize(name);
  }

  getProductImage() {
    const image = this.props.product.pictures[this.state.imagePage - 1];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  getProductBrand() {
    const aux = this.props.product.brands;
    if (aux !== [] && aux[0] !== undefined) {
      return aux[0].name;
    }
    return '';
  }

  getProductPrice() {
    return roundUp(this.props.product.priceWithVat, 2) + '€';
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
      return aux + "mL";
    } else {
      return 'N/A';
    }
  }

  goToLeftImage() {
    let aux = this.state.imagePage;
    if (aux > 1) {
      this.setState({
        imagePage: aux - 1,
      });
    }
  }

  goToRightImage() {
    let aux = this.state.imagePage;
    if (aux < this.props.product.pictures.length) {
      this.setState({
        imagePage: aux + 1,
      });
    }
  }

  getProductCategories() {
    let categories = this.props.product.categories;
    if (categories.length > 0) {
      categories = categories.map((category) => category.name + '/ ');
      const pos = categories.length - 1;
      categories[pos] = categories[pos].slice(0, categories[pos].length - 2);
      return categories;
    } else {
      return "N/A";
    }
  }

  isProductStocked() {
    const { t } = this.props;
    const aux = this.props.product.availableStock;
    if (aux > 0) {
      return t('yes') + ' (' + aux + ')';
    } else {
      return t('no');
    }
  }

  zoomImage() {
    this.props.zoomImage(this.props.product.pictures[this.state.imagePage - 1]);
  }

  getMoreInfo() {
    const { t } = this.props;
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
          <div className="details-line">
          </div>
          <div className="details-box">
            <div className="details-info">{this.getMoreInfo()}</div>
          </div>
          <div className="details-buttons">
            <img className="details-close-button-image"
              src={closeButton}
              onClick={this.closeDetailsPanel}
            >
            </img>
            <img className="details-map-button-image" src={mapButton}>
            </img>
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
