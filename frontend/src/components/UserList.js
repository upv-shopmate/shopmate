/* eslint react/prop-types: 0 */
/* eslint no-irregular-whitespace: 2*/

import '../assets/css/UserList.css';
import React from 'react';
import {withTranslation} from 'react-i18next';

class UserList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      price: 0.0,
      quantity: this.props.entries.length,
      price: this.props.list.totalPrice.toFixed(2)
    };
  }

  render() {
    const {t} = this.props;
    return (
      <div className="user-list-wrapper"
        onClick={() => this.props.onListClick(this.props.list)}
      >
        <div className="user-list-name">{this.props.name}</div>
        <div className="user-list-quantity">
          {this.state.quantity} {t('products')}
        </div>
        <div className="user-list-price">{this.state.price}€</div>
      </div>
    ); 
  }
}

export default withTranslation()(UserList);
