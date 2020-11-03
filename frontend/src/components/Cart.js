import '../assets/css/Cart.css';
import React from 'react';
import CartProduct from './CartProduct';

class Cart extends React.Component {
  constructor(props) {
    super(props);
  }

  renderProducts() {
    return this.props.results.map((product) =>
      <Result key={result.barcode} productData={product} />,
    );
  }

  getProductsListNumber(){
    return 'Dentro de este carro (1)';
  }

  render() {
    return (
      <div className="product-list">
        <div className="cart-title">{this.getProductsListNumber()}</div>
        <div className="cart-products">
          <CartProduct>

          </CartProduct>
        </div>
        <div className="total-prices">
          <div className="subtotal-block">
            <div className="subtotal">Subtotal: 1,98 €</div>
            <div className="subtotal-import">IVA 21% Base: 1,98€ Importe: 0,42 €</div>
          </div>
          <div className="cart-info">
            <div className="cart-articles">1 artículos en el carro</div>
            <div className="planned-articles">9 artículos planificados</div>
            <div className="coupons">Ningún cupón aplicado</div>
          </div>
          <div className="final-price">
            <div className="total">Total</div>
            <div className="price">2,40 €</div>
            <div className="planned-price">Planificado: 12,40 €</div>
          </div>
        </div>   
      </div>
    );
  }
}

export default Cart;
