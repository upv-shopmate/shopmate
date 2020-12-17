/* eslint react/prop-types: 0 */

import '../assets/css/Cart.css';
import React from 'react';
import CartProduct from './CartProduct';
import PaymentPopup from './PaymentPopup';
import {withTranslation} from 'react-i18next';
import {getStore} from '../utils/Store';
import {roundUp} from '../utils/Utils';
import {requestProductByBarcode} from '../requests/ProductRequest';

class Cart extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      showPopup: false,
    };
    this.togglePopup = this.togglePopup.bind(this);
    this.getProductTotalPrice = this.getProductTotalPrice.bind(this);
    window.cart = this;
  }

  componentDidMount() {
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
  }

  getProductSubtotal() {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined) {
      return roundUp(cartContent.subtotalPrice, 2);
    } else {
      return 0;
    }
  }

  getProductTotalPrice() {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined) {
      return (roundUp(cartContent.totalPrice, 2));
    } else {
      return 0;
    }
  }

  getProductPriceBase(index) {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined && cartContent.modifierBreakdowns.length > 0) {
      return ' Base: ' + roundUp(
          cartContent.modifierBreakdowns[index].applicableBase, 2,
      );
    } else {
      return ' Base: 0';
    }
  }

  getProductModifier(index) {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined && cartContent.modifierBreakdowns.length > 0) {
      return this.getProductModifierText() +
      cartContent.modifierBreakdowns[index].modifier.value * 100;
    } else {
      return this.getProductModifierText() + 0;
    }
  }

  getProductModifierText(index) {
    const {t} = this.props;
    let text = t('cart.iva');
    if (index > 0) {
      text = this.props.cartContent.modifierBreakdowns[index].modifier.code + ': ';
    }
    return text;
  }

  getProductPriceImport() {
    const {t} = this.props;
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined && cartContent.modifierBreakdowns.length > 0) {
      return t('cart.ammount') + roundUp(
          cartContent.modifierBreakdowns[0].totalDelta, 2,
      );
    } else {
      return t('cart.ammount') + 0;
    }
  }

  getProductsListNumber() {
    if (this.props.cartContent !== undefined) {
      let numberOfProducts = 0;
      this.props.cartContent.entries.forEach((product) => {
        numberOfProducts = numberOfProducts + product.quantity;
      });
      return numberOfProducts;
    } else {
      return 0;
    }
  }

  getCurrentListProducts() {
    if (this.getCurrentList() == null) {
      return (0);
    } else {
      return (this.getCurrentList().entries.length);
    }
  }

  getPlannedPrice() {
    if (this.getCurrentList() == null) {
      return (0);
    } else {
      return (this.getCurrentList().totalPrice.toFixed(2));
    }
  }

  renderContents() {
    if (this.props.cartContent !== undefined) {
      return this.props.cartContent.entries.map((entry) =>
        <CartProduct
          key={entry.item.barcode}
          entry={entry}
        />,
      );
    }
  }

  getCurrentList() {
    const store = getStore();
    return store.getState().currentList;
  }

  async addProduct(barcode) {
    let answerMessage;
    try {
      if (this.props.cartContent !== undefined) {
        const product = await requestProductByBarcode(barcode);
        this.props.addProductToCartContent(product);
        answerMessage = 'El producto ha sido añadido satisfactoriamente.';
      }
    } catch (e) {
      answerMessage = 'El producto solicitado no ha podido ser añadido al carro,' +
        ' asegúrese de que el código introducido es correcto.';
    }
    return answerMessage;
  }

  async removeProduct(barcode) {
    let answerMessage;
    try {
      if (this.props.cartContent !== undefined) {
        const product = await requestProductByBarcode(barcode);
        this.props.removeProductToCartContent(product);
        answerMessage = 'El producto ha sido removido satisfactoriamente.';
      }
    } catch (e) {
      answerMessage = 'El producto solicitado no ha podido ser removido del carro,' +
        ' asegúrese de que el código introducido es correcto y se encuentra en el carro.';
    }
    return answerMessage;
  }

  getCartContent() {
    return this.props.cartContent;
  }

  togglePopup() {
    this.setState({
      showPopup: !this.state.showPopup,
    });
  }

  getAppliedCouponsText() {
    const {t} = this.props;
    let text;
    const count = this.props.appliedCoupons.length;
    if (count === 0) {
      text = t('cart.appliedCoupons.none');
    } else if (count === 1) {
      text = t('cart.appliedCoupons.one', {count});
    } else if (count > 1) {
      text = t('cart.appliedCoupons.plural', {count});
    }
    return text;
  }

  renderPaymentPopup() {
    if (this.state.showPopup === true) {
      return <PaymentPopup
        togglePopup={this.togglePopup}
        getProductTotalPrice={this.getProductTotalPrice}
        cartContent={this.props.cartContent}
        resetCartContent={this.props.resetCartContent}>
      </PaymentPopup>;
    } else return null;
  }

  renderModifierBreakdown() {
    let breakdown;
    this.props.cartContent.modifierBreakdowns.forEach((modifier) => {
      const index = this.props.cartContent.modifierBreakdowns.indexOf(modifier);
      if (index < 3) {
        breakdown = breakdown + (
          <div className="subtotal-import">
            {this.getProductModifier(index)} %
            {this.getProductPriceBase(index)} €
            {this.getProductPriceImport(index)} €
          </div>
        );
      }
    });
    return breakdown;
  }

  render() {
    const {t} = this.props;
    return (
      <div className="product-list">
        <div className="cart-title">
          {t('cart.insideCart')} ({this.getProductsListNumber()})
        </div>
        <div className="cart-products">{this.renderContents()}
        </div>
        <div className="total-prices"
          onClick={this.togglePopup}>
          <div className="subtotal-block">
            <div className="subtotal">
              Subtotal: {this.getProductSubtotal()} €
            </div>
            <div className="subtotal-import">
              {this.getProductModifier(0)} %
              {this.getProductPriceBase(0)} €
              {this.getProductPriceImport(0)} €
            </div>
            {/* {this.renderModifierBreakdown()} */}
          </div>
          <div className="cart-info">
            <div className="cart-articles">
              {t('cart.articlesCart', {count: this.getProductsListNumber()})}
            </div>
            <div className="planned-articles">
              {t('cart.plannedArticles', {articles: this.getCurrentListProducts()})}
            </div>
            <div className="coupons">{this.getAppliedCouponsText()}</div>
          </div>
          <div className="final-price">
            <div className="total">Total</div>
            <div className="price">{this.getProductTotalPrice()} € </div>
            <div className="planned-price">
              {t('cart.estimated')} {this.getPlannedPrice()} €
            </div>
          </div>
        </div>
        {this.renderPaymentPopup()}
      </div>
    );
  }
}

export default withTranslation()(Cart);
