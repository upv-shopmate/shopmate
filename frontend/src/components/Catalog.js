/* eslint react/prop-types: 0 */

import '../assets/css/Catalog.css';
import React from 'react';
import './Product';
import Product from './Product';
import ProductDetails from './ProductDetails';
import pageLeftImage from '../assets/images/page-left.png';
import pageRightImage from '../assets/images/page-right.png';
import CategoriesDropDown from './CategoriesDropDown';
import { requestCatalogByCategory } from '../requests/ProductRequest';

const HEIGHTS = {
  OPENED: '55%', // products height
  CLOSED: '82%',
};

class Catalog extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      selectedProduct: null,
      selectedCategory: undefined,
    };
    this.showProductDetails = this.showProductDetails.bind(this);
    this.closeProductDetails = this.closeProductDetails.bind(this);
  }

  updateCategory(category) {
    this.setState({
      selectedCategory: category,
    });
  }

  componentDidMount() {
    this.startCatalog();
  }

  startCatalog() {
    if (this.props.catalog !== []) {
      this.setState({
        'products': this.props.catalog,
      });
    }
  }

  updateCatalog(catalog) {
    this.setState({
      'products': catalog,
    });
  }

  changePanelHeight(newHeight) {
    const productsPanel = document.querySelector('.products');
    if (productsPanel !== null) {
      productsPanel.style.height = newHeight;
    }
  }

  showProductDetails(product) {
    if (product !== null) {
      this.setState({
        products: this.state.products,
        selectedProduct: product,
      });
      this.changePanelHeight(HEIGHTS.OPENED);
    }
  }

  closeProductDetails() {
    this.setState({
      products: this.state.products,
      selectedProduct: null,
    });
    this.changePanelHeight(HEIGHTS.CLOSED);
  }

  handleClickLeft() {
    const newPage = this.props.page - 1;
    if (newPage > 0) {
      this.props.onGoToPage(newPage);
    }
  }
  handleClickRight() {
    const newPage = this.props.page + 1;
    this.props.onGoToPage(newPage);
  }

  renderProducts() {
    if (this.state.products.length > 0) {
      return this.state.products.map((product) =>
        <Product
          key={product.barcode}
          productData={product}
          showProductDetails={this.showProductDetails}
        />,
      );
    }
  }

  render() {
    return (
      <div className="catalog">
        <div className="products">
          {this.renderProducts()}
        </div>
        <div className="catalog-bottom">
          <CategoriesDropDown></CategoriesDropDown>
          <div className="catalog-nav">
            <img src={pageLeftImage} onClick={() => this.handleClickLeft()} />
            <div className="page-number">{this.props.page}</div>
            <img src={pageRightImage} onClick={() => this.handleClickRight()} />
          </div>
        </div>
        <div className="product-details">
          <ProductDetails
            zoomImage={this.props.zoomImage}
            closePanel={this.closeProductDetails}
            product={this.state.selectedProduct}
          />
        </div>
      </div>
    );
  }
}

export default Catalog;
