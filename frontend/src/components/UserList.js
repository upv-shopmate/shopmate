/* eslint react/prop-types: 0 */
/* eslint no-irregular-whitespace: 2*/

import '../assets/css/UserList.css';
import React from 'react';

class UserList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      price: 0.0,
      quantity: this.props.entries.length,
      price: this.computePlannedPrice(), // FIXME remove
    };
  }
  // FIXME this should be removed since it's inside userlist
  // data from the database
  computePlannedPrice() {
    let price = 0.0;
    this.props.entries.forEach((product) => {
      price += product.priceWithVat;
    });
    return price.toFixed(2);
  }

  render() {
    return (
      <div className="user-list-wrapper"
        onClick={() => this.props.onListClick(this.props.list)}
      >
        <div className="user-list-name">{this.props.name}</div>
        <div className="user-list-quantity">
          {this.state.quantity} productos
        </div>
        <div className="user-list-price">{this.state.price}€</div>
      </div>
    ); 
  }
}

export default UserList;
