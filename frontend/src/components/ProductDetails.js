import "../assets/css/ProductDetails.css";
import closeButton from "../assets/images/close_button.png";
import mapButton from "../assets/images/map_button.png"
import imageNotFound from "../assets/images/image_not_found.jpg"
import React, { Component } from "react";

class ProductDetails extends Component {
  constructor(props) {
    super(props);
    this.viewDetails = this.viewDetails.bind(this);
    this.closeDetailsPanel = this.closeDetailsPanel.bind(this);
  }

  getProductName() {
    return this.props.product.name.charAt(0).toUpperCase() + this.props.product.name.slice(1);
  }

  getProductImage() {
    let image = this.props.product.pictures[0];
    if (image !== undefined) {
      return image;
    } else {
      return imageNotFound;
    }
  }

  getProductBrand() {
    let aux = this.props.product.brands;
    if (aux !== [] && aux[0] !== undefined) {
      return "de " + aux[0].name;
    }
    return "";
  }

  getProductPrice() {
    return this.props.product.price.toFixed(2) + "€";
  }

  closeDetailsPanel() {
    this.props.closePanel();
  }

  getProductWeight() {
    let aux = this.props.product.weight;
    if (aux !== null) {
      return aux + "g";
    } else {
      return "N/A";
    }
  }

  getProductOriginCountry() {
    let aux = this.props.product.originCountry;
    if (aux !== null) {
      return aux;
    } else {
      return "N/A";
    }
  }

  getProductVolume() {
    let aux = this.props.product.volume;
    if (aux !== null) {
      return aux;
    } else {
      return "N/A";
    }
  }

  isProductStocked() {
    let aux = this.props.product.availableStock;
    if (aux > 0) {
      return "Si (" + aux + ")";
    } else {
      return "No";
    }
  }

  isProductEdible() {
    if (this.props.product.edible) {
      return "Si";
    } else {
      return "No";
    }
  }

  getMoreInfo() {
    return (
      <React.Fragment>
        <div className="details-text-line">
          <span className="details-text-header">Peso: </span>
          <span className="details-text">{this.getProductWeight()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">Volumen: </span>
          <span className="details-text">{this.getProductVolume()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">País de origen: </span>
          <span className="details-text">{this.getProductOriginCountry()}</span>
        </div>
        <div className="details-text-line">
          <span className="details-text-header">En stock: </span>
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
            <div className="details-close-button"><img className="details-close-button-image" src={closeButton} onClick={this.closeDetailsPanel} ></img></div>
            <div className="details-info">{this.getMoreInfo()}</div>
            <div className="details-map-button"><img className="details-map-button-image" src={mapButton}></img></div>
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

export default ProductDetails;
