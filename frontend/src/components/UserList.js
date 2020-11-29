/* eslint react/prop-types: 0 */
/* eslint no-irregular-whitespace: 2*/

import '../assets/css/UserList.css';
import React from 'react';
import {withTranslation} from 'react-i18next';
import { Store } from '../utils/Store';

class UserList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      price: 0.0,
      quantity: this.props.entries.length,
      price: this.props.list.totalPrice.toFixed(2),
    };
  }

  changeCurrentList() {
    let store = Store().getInstance();
    store.dispatch({
      type: "changeCurrentList",
      currentList: this.props.list
    })
  }

  render() {
    const {t} = this.props;
    return (
      <div className="user-list-wrapper"
        onClick={() => this.changeCurrentList()}
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
