import '../assets/css/Cart.css';
import React from 'react';
import CartProduct from './CartProduct';
import {requestCartContentDataBase} from '../requests/CartContents';

class Cart extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'cartContent': null,
    };
  }

  componentDidMount() {
    this.getProductsFromDataBase();
  }

  async getProductsFromDataBase() {
    const data = await requestCartContentDataBase();
    this.setState({
      cartContent: data,
    });
  }

  getProductSubtotal() {
    const cartContent = this.state.cartContent;
    return (cartContent ?
      cartContent.subtotalPrice.toFixed(2) : 0);
  }

  getProductTotalPrice() {
    const cartContent = this.state.cartContent;
    return (cartContent ?
      cartContent.totalPrice.toFixed(2) : 0);
  }

  getProductPriceBase() {
    const cartContent = this.state.cartContent;
    return ' Base: ' + (cartContent ?
      cartContent.modifierBreakdowns[0].applicableBase.toFixed(2) : 0);
  }

  getProductIVA() {
    const cartContent = this.state.cartContent;
    return 'IVA ' + (cartContent ?
      cartContent.modifierBreakdowns[0].modifier.value * 100 : 0);
  }

  getProductPriceImport() {
    const cartContent = this.state.cartContent;
    return ' Importe: ' + (cartContent ?
      cartContent.modifierBreakdowns[0].totalDelta.toFixed(2) : 0);
  }

  getProductsListNumber() {
    return this.state.cartContent ?
      this.state.cartContent.entries.length : 0;
  }

  renderContents() {
    if (this.state.cartContent) {
      return this.state.cartContent.entries.map((entry) =>
        <CartProduct
          key={entry.item.barcode}
          entry={entry}
        />,
      );
    }
  }

  render() {
    return (
      <div className="product-list">
        <div className="cart-title">
          Dentro de este carro ({this.getProductsListNumber()})
        </div>
        <div className="cart-products">{this.renderContents()}
        </div>
        <div className="total-prices">
          <div className="subtotal-block">
            <div className="subtotal">
              Subtotal: {this.getProductSubtotal()} €
            </div>
            <div className="subtotal-import">
              {this.getProductIVA()} %
              {this.getProductPriceBase()} €
              {this.getProductPriceImport()} €
            </div>
          </div>
          <div className="cart-info">
            <div className="cart-articles">
              {this.getProductsListNumber()} artículos en el carro
            </div>
            <div className="planned-articles">N/A artículos planificados</div>
            <div className="coupons">Ningún cupón aplicado</div>
          </div>
          <div className="final-price">
            <div className="total">Total</div>
            <div className="price">{this.getProductTotalPrice()} € </div>
            <div className="planned-price">Planificado: N/A €</div>
          </div>
        </div>
      </div>
    );
  }
}

export default Cart;
