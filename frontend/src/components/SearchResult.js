import "../assets/css/SearchResult.css";
import tagIcon from "../assets/images/tag_icon.png"
import imageNotFound from "../assets/images/image_not_found.jpg"
import React, { Component } from 'react';

class SearchResult extends Component {
    constructor(props) {
        super(props);
    }

    getProductName() {
        return this.props.productData.name.charAt(0).toUpperCase() + this.props.productData.name.slice(1);
    }

    getProductImage() {
        let image = this.props.productData.pictures[0];
        if (image !== undefined) {
            return image;
        } else {
            return imageNotFound;
        }
    }

    getProductBrand() {
        let aux = this.props.productData.brands;
        if (aux !== [] && aux[0] !== undefined) {
            return "de " + aux[0].name;
        }
        return "N/A";
    }

    getProductPrice() {
        return this.props.productData.price.toFixed(2) + "€";
    }

    getProductWeightOrVolume() {
        let aux = this.props.productData.weight;
        if (aux !== null) {
            return aux + "g";
        } else {
            aux = this.props.productData.volume;
            if (aux !== null) {
                return aux + "L"
            }
            return "N/A";
        }
    }

    getProductCategories() {
        let categories = this.props.productData.categories;
        if (categories.length > 0) {
            categories = categories.map(categorie => categorie.name + ", ");
            return categories.slice(0, categories.length - 2);
        } else {
            return "Sin categorías"
        }

    }

    render() {
        return (
            <div className="result">
                <img className="result-image" src={this.getProductImage()}></img>
                <div className="result-info">
                    <div className="result-topline">
                        <div className="result-name">{this.getProductName()}</div>
                        <div className="result-separator">·</div>
                        <div className="result-brand">{this.getProductBrand()}</div>
                        <div className="result-separator">·</div>
                        <div className="result-weight">{this.getProductWeightOrVolume()}</div>
                    </div>
                    <div className="result-bottomline">
                        <div className="result-cart">En el carro: N/A</div>
                        <div className="result-categories">
                            <img className="result-categories-image" src={tagIcon}></img>
                            <div className="result-categories-namelist">{this.getProductCategories()}</div>
                        </div>
                    </div>
                </div>
                <div className="result-price">{this.getProductPrice()}</div>
            </div>
        );
    }
}

export default SearchResult;