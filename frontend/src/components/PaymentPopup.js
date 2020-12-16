/* eslint react/prop-types: 0 */

import check from '../assets/images/check_payment.png';
import xCross from '../assets/images/x_cross_payment.png';
import leftArrow from '../assets/images/left_arrow.png';
import '../assets/css/PaymentPopup.css';
import React from 'react';
import {withTranslation} from 'react-i18next';
import {roundUp} from '../utils/Utils';


class PaymentPopup extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'paymentMenuActive': true,
      'paymentProcessed': false,
      'paymentSuccessful': false,
    };
    this.processPayment = this.processPayment.bind(this);
    this.returnToPaymentMenu = this.returnToPaymentMenu.bind(this);
    this.closePopup = this.closePopup.bind(this);
  }

  processPayment() {
    this.setState({
      'paymentMenuActive': false,
    });
    // pending real integration with payment platform
    setTimeout(() => {
      this.props.resetCartContent();
      this.setState({
        'paymentProcessed': true,
        'paymentSuccessful': true, // comment this line to see the payment fail message
      });
    }, 3000);
  }

  returnToPaymentMenu() {
    this.setState({
      'paymentMenuActive': true,
      'paymentProcessed': false,
      'paymentSuccessful': false,
    });
  }

  closePopup() {
    this.returnToPaymentMenu();
    this.props.togglePopup();
  }

  fillTextPartWithWhitespaces(text, length, onRight) {
    if (text.length > length) {
      text = text.substring(0, length);
    } else {
      if (onRight) {
        text = new Array(length - text.length + 1).join(' ') + text;
      } else {
        text = text + new Array(length - text.length + 1).join(' ');
      }
    }
    return text;
  }

  getTextCartContent() {
    const cartList = this.props.cartContent;
    let text = new Array(32).join('*') + '\n';
    if (cartList !== undefined) {
      cartList.entries.forEach((element) => {
        text = text +
                    this.fillTextPartWithWhitespaces(element.quantity.toString(), 4) +
                    this.fillTextPartWithWhitespaces(element.item.name, 20) +
                    this.fillTextPartWithWhitespaces(roundUp(element.totalPrice, 2), 7, true) +
                    '\n';
      });
      text = text + new Array(32).join('*');
    }
    return text;
  }

  getTitle() {
    const {t} = this.props;
    let title = '';
    if (this.state.paymentMenuActive) {
      title = t('paymentMenu');
    } else {
      if (this.state.paymentProcessed) {
        if (this.state.paymentSuccessful) {
          title = t('paymentFinished');
        } else {
          title = t('paymentFailed');
        }
      } else {
        title = t('paymentInProcess');
      }
    }
    return title;
  }

  getIcon() {
    if (this.state.paymentSuccessful) {
      return <img className="payment-message-icon" src={check} />;
    } else if (this.state.paymentProcessed) {
      return <img className="payment-message-icon" src={xCross} />;
    }
  }

  getMessage() {
    const {t} = this.props;
    let message = '';
    if (this.state.paymentProcessed) {
      if (this.state.paymentSuccessful) {
        message = t('paymentFinishedMessage');
      } else {
        message = t('paymentFailedMessage');
      }
    } else {
      message = t('paymentInProcessMessage');
    }
    return message;
  }

  renderTop() {
    return (
      <div className="top-payment-popup">
        {this.getTitle()}
      </div>
    );
  }

  renderPaymentMenu() {
    const {t} = this.props;
    const total = t('totalToPay') + '\n ';
    const totalPrice = this.props.getProductTotalPrice() + ' €';
    return (
      <div className="body-payment-popup">
        <div className="product-list-payment-popup">
          {this.getTextCartContent()}
        </div>
        <div className="confirmation-menu-payment-popup">
          <div className="ammount-payment-popup">
            {total}
          </div>
          <div className="ammount-payment-popup-price">
            {totalPrice}
          </div>
          <div className="confirmation-button-box-payment-popup">
            <button className="confirmation-button-payment-popup"
              disabled={this.props.getProductTotalPrice()==0}
              onClick={this.processPayment}>
              {t('confirmPayment')}
            </button>
          </div>
        </div>
      </div>
    );
  }

  renderPaymentMessage() {
    return (
      <div className="body-payment-message-popup">
        {this.getIcon()}
        {this.getMessage()}
      </div>
    );
  }

  renderBody() {
    if (this.state.paymentMenuActive) {
      return this.renderPaymentMenu();
    } else {
      return this.renderPaymentMessage();
    }
  }

  renderBottomPaymentMenu() {
    const {t} = this.props;
    return (
      <div className="bottom-payment-popup">
        <button className="return-button-payment-popup"
          onClick={this.closePopup}>
          <img className="return-button-img" src={leftArrow}></img>
          <div className="return-button-text">{t('return')}</div>
        </button>
      </div>
    );
  }

  renderBottomPaymentMessage() {
    const {t} = this.props;
    return (
      <div className="bottom-payment-popup">
        <button className="cancel-button-payment-popup"
          onClick={this.closePopup}>
          {t('cancel')}
        </button>
        <button className="try-again-button-payment-popup"
          onClick={this.returnToPaymentMenu}>
          {t('tryAgain')}
        </button>
      </div>
    );
  }

  renderBottom() {
    if (this.state.paymentMenuActive || this.state.paymentSuccessful) {
      return this.renderBottomPaymentMenu();
    } else if (this.state.paymentProcessed) {
      return this.renderBottomPaymentMessage();
    }
  }

  render() {
    return (
      <div className="payment-popup">
        {this.renderTop()}
        {this.renderBody()}
        {this.renderBottom()}
      </div>
    );
  }
}

export default withTranslation()(PaymentPopup);
