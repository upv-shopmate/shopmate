import "../assets/css/Catalog.css";
import React from "react";
import "./Product";
import { requestDataBase } from '../requests/ProductRequest.js'
import Product from "./Product";
import ProductDetails from "./ProductDetails";

const HEIGHTS = {
  OPENED: '60%',
  CLOSED: '100%'
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
    this.getProductsFromDataBase = this.getProductsFromDataBase.bind(this);
  }

  componentDidMount() {
    this.getProductsFromDataBase();
  }

  async getProductsFromDataBase() {
    let data = await requestDataBase();
    this.setState({
      products: data,
      selectedProduct: this.state.selectedProduct
    })
    this.renderProducts(data);
  }

  changePanelHeight(newHeight) {
    const productsPanel = document.querySelector(".products");
    if (productsPanel !== null) {
      productsPanel.style.height = newHeight;
    }
  }

  showProductDetails(product) {
    if (product !== null) {
      this.setState({
        products: this.state.products,
        selectedProduct: product
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

  renderProducts() {
    return this.state.products.map(product => <Product key={product.barcode} productData={product} showProductDetails={this.showProductDetails} />);
  }

  render() {
    return (
      <div className="catalog">
        <div className="products">{this.renderProducts()}</div>
        <div className="product-details">
          <ProductDetails closePanel={this.closeProductDetails} product={this.state.selectedProduct} />
        </div>
      </div>
    );
  }
}

export default Catalog;
