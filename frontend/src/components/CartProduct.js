import '../assets/css/CartProduct.css';
import imageNotFound from '../assets/images/image_not_found.jpg';
import React, {Component} from 'react';
import {capitalize} from '../utils/Utils';
import {requestCartContentDataBase} from '../requests/CartContents'

class CartProduct extends Component{
    constructor(props) {
        super(props);
      }

      render() {
        const product = this.props.entry.item;
        return (
          <div className="product-entrie">
            <img className="entrie-image" src={product.pictures[0]}></img>
            <div className="entrie-info">
              <div className="entrie-topline">
                <div className="entrie-name">{capitalize(product.name)}</div>
              </div>
              <div className="entrie-bottomline">
                <div className="entrie-quantity">Cantidad: {this.props.entry.quantity}</div>
              </div>
            </div>
            <div className="entrie-price">{product.priceWithVat.toFixed(2)} €</div>
          </div>
        );
      }
    }

    export default CartProduct;