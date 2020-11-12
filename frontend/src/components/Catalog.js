/* eslint react/prop-types: 0 */

import '../assets/css/Catalog.css';
import React from 'react';
import './Product';
import Product from './Product';
import ProductDetails from './ProductDetails';

const HEIGHTS = {
  OPENED: '60%',
  CLOSED: '100%',
};

class Catalog extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      selectedProduct: null,
    };
    this.showProductDetails = this.showProductDetails.bind(this);
    this.closeProductDetails = this.closeProductDetails.bind(this);
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

  handleScroll(element) {
    const bottomPosition = (element.target.scrollHeight -
      element.target.offsetHeight);
    const currentPosition = element.target.scrollTop;
    if (currentPosition === bottomPosition) {
      this.props.onBottomPage();
    }
  }

  renderProducts() {
    if (this.state.products !== []) {
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
        <div className="products"
          onScroll={(e) => this.handleScroll(e)}>{this.renderProducts()}
        </div>
        <div className="product-details">
          <ProductDetails
            closePanel={this.closeProductDetails}
            product={this.state.selectedProduct}
          />
        </div>
      </div>
    );
  }
}

export default Catalog;
