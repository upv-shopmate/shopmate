/* eslint react/prop-types: 0 */

import '../assets/css/Cart.css';
import React from 'react';
import CartProduct from './CartProduct';
import {requestCartContentDataBase} from '../requests/CartContents';
import { withTranslation } from 'react-i18next';

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

  roundUp(num, precision) {
    precision = Math.pow(10, precision);
    return (Math.ceil(num * precision) / precision).toFixed(2);
  }

  async getProductsFromDataBase() {
    let data;
    try {
      data = await requestCartContentDataBase();
      this.props.hideErrorPanel();
      this.setState({
        cartContent: data,
      });
    } catch (e) {
      this.getProductsFromDataBase();
      this.props.showErrorPanel();
    }
  }

  getProductSubtotal() {
    const cartContent = this.state.cartContent;
    return (cartContent ?
      this.roundUp(cartContent.subtotalPrice, 2) : 0);
  }

  getProductTotalPrice() {
    const cartContent = this.state.cartContent;
    return (cartContent ?
      this.roundUp(cartContent.totalPrice, 2) : 0);
  }

  getProductPriceBase() {
    const cartContent = this.state.cartContent;
    return ' Base: ' + (cartContent ?
      this.roundUp(cartContent.modifierBreakdowns[0].applicableBase, 2) : 0);
  }

  getProductIVA(t) {
    const cartContent = this.state.cartContent;
    return t('iva') + (cartContent ?
      cartContent.modifierBreakdowns[0].modifier.value * 100 : 0);
  }

  getProductPriceImport(t) {
    const cartContent = this.state.cartContent;
    return t('ammount') + (cartContent ?
      this.roundUp(cartContent.modifierBreakdowns[0].totalDelta, 2) : 0);
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
    const { t, i18n } = this.props;
    return (
      <div className="product-list">
        <div className="cart-title">
          {t("insideCart")} ({this.getProductsListNumber()})
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
              {t("articlesCart", {articles: 2/*this.getProductsListNumber()*/})} 
            </div>
            <div className="planned-articles">{t("plannedArticles")}</div>
            <div className="coupons">{t("appliedCoupons")}</div>
          </div>
          <div className="final-price">
            <div className="total">Total</div>
            <div className="price">{this.getProductTotalPrice()} € </div>
            <div className="planned-price">{t("estimated")} N/A €</div>
          </div>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Cart);
