/* eslint react/prop-types: 0 */
import '../assets/css/SearchResult.css';
import tagIcon from '../assets/images/tag_icon.png';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';
import {withTranslation} from 'react-i18next';

class SearchResult extends Component {
  constructor(props) {
    super(props);
  }

  roundUp(num, precision) {
    precision = Math.pow(10, precision);
    return (Math.ceil(num * precision) / precision).toFixed(2);
  }

  getProductName() {
    const name = this.props.productData.name;
    return capitalize(name);
  }

  getProductImage() {
    const image = this.props.productData.pictures[0];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  getProductBrand() {
    const aux = this.props.productData.brands;
    if (aux !== [] && aux[0] !== undefined) {
      return aux[0].name;
    }
    return 'N/A';
  }

  getProductPrice() {
    return this.roundUp(this.props.productData.priceWithVat, 2) + '€';
  }

  getProductWeightOrVolume() {
    let aux = this.props.productData.weight;
    if (aux !== null) {
      return aux + 'g';
    } else {
      aux = this.props.productData.volume;
      if (aux !== null) {
        return aux + 'L';
      }
      return 'N/A';
    }
  }

  getProductCategories() {
    const {t} = this.props;
    let categories = this.props.productData.categories;
    if (categories.length > 0) {
      categories = categories.map((category) => category.name + ', ');
      const pos = categories.length - 1;
      categories[pos] = categories[pos].slice(0, categories[pos].length - 2);
      return categories;
    } else {
      return t('withoutCategories');
    }
  }

  render() {
    const {t} = this.props;
    return (
      <div className="result">
        <img className="result-image" src={this.getProductImage()}></img>
        <div className="result-info">
          <div className="result-topline">
            <div className="result-name">{this.getProductName()}</div>
            <div className="result-separator">·</div>
            <div className="result-brand">{this.getProductBrand()}</div>
            <div className="result-separator">·</div>
            <div className="result-weight">
              {this.getProductWeightOrVolume()}
            </div>
          </div>
          <div className="result-bottomline">
            <div className="result-cart">{t('inCart')} N/A</div>
            <div className="result-categories">
              <img className="result-categories-image" src={tagIcon}></img>
              <div className="result-categories-namelist">
                {this.getProductCategories()}
              </div>
            </div>
          </div>
        </div>
        <div className="result-price">{this.getProductPrice()}</div>
      </div>
    );
  }
}

export default withTranslation()(SearchResult);
