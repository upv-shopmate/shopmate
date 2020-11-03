import '../assets/css/CartProduct.css';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';

class CartProduct extends Component{
    constructor(props) {
        super(props);
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
          return 'de ' + aux[0].name;
        }
        return 'N/A';
      }
    
      getProductPrice() {
        return this.props.productData.price.toFixed(2) + '€';
      }

      render() {
        return (
          <div className="product-entrie">
            <img className="entrie-image" src={"https://static.carrefour.es/hd_510x_/img_pim_food/001775_00_1.jpg"}></img>
            <div className="entrie-info">
              <div className="entrie-topline">
                <div className="entrie-name">Nesquik</div>
              </div>
              <div className="entrie-bottomline">
                <div className="entrie-quantity">Cantidad: N/A</div>
              </div>
            </div>
            <div className="entrie-price">2,40 €</div>
          </div>
        );
      }
    }

    export default CartProduct;