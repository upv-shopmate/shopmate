/* eslint react/prop-types: 0 */

import '../assets/css/Cart.css';
import React from 'react';
import CartProduct from './CartProduct';
import {withTranslation} from 'react-i18next';
import {getStore} from '../utils/Store';
import {roundUp} from '../utils/Utils';

class Cart extends React.Component {
  constructor(props) {
    super(props);
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

  getProductPriceBase() {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined) {
      return ' Base: ' + roundUp(
          cartContent.modifierBreakdowns[0].applicableBase, 2,
      );
    } else {
      return ' Base: 0';
    }
  }

  getProductIVA(t) {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined) {
      return t('iva') +
      cartContent.modifierBreakdowns[0].modifier.value * 100;
    } else {
      return t('iva') + 0;
    }
  }

  getProductPriceImport(t) {
    const cartContent = this.props.cartContent;
    if (cartContent !== undefined) {
      return t('ammount') + roundUp(
          cartContent.modifierBreakdowns[0].totalDelta, 2,
      );
    } else {
      return t('ammount') + 0;
    }
  }

  getProductsListNumber() {
    if (this.props.cartContent !== undefined) {
      return this.props.cartContent.entries.length;
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

  render() {
    const {t} = this.props;
    return (
      <div className="product-list">
        <div className="cart-title">
          {t('insideCart')} ({this.getProductsListNumber()})
        </div>
        <div className="cart-products">{this.renderContents()}
        </div>
        <div className="total-prices">
          <div className="subtotal-block">
            <div className="subtotal">
              Subtotal: {this.getProductSubtotal()} €
            </div>
            <div className="subtotal-import">
              {this.getProductIVA(t)} %
              {this.getProductPriceBase()} €
              {this.getProductPriceImport(t)} €
            </div>
          </div>
          <div className="cart-info">
            <div className="cart-articles">
              {t('articlesCart', {count: this.getProductsListNumber()})}
            </div>
            <div className="planned-articles">
              {t('plannedArticles', {articles: this.getCurrentListProducts()})}
            </div>
            <div className="coupons">{t('appliedCoupons')}</div>
          </div>
          <div className="final-price">
            <div className="total">Total</div>
            <div className="price">{this.getProductTotalPrice()} € </div>
            <div className="planned-price">
              {t('estimated')} {this.getPlannedPrice()} €
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Cart);
