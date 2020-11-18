/* eslint react/prop-types: 0 */

import '../assets/css/NotLoginPanel.css';
import accountImage from '../assets/images/account_circle.png';
import React from 'react';

class NotLoginPanel extends React.Component {
  constructor(props) {
    super(props);
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  getLoginText() {
    return ('Inicia sesión para ver tus listas de la compra,' +
      ' cupones y ofertas personalizadas.');
  }

  render() {
    return (
      <div>
        <div className="not-logged-user-block">
          <img className="account-image" src={accountImage}></img>
          <div className="question"> ¿Ya tienes cuenta de cliente? </div>
          <div className="login-text">{this.getLoginText()}</div>
          <button
            className="left-panel-login-button"
            onClick={this.openLoginPanel.bind(this)}
          >INICIAR SESIÓN</button>
        </div>
      </div>
    );
  }
}

export default NotLoginPanel;
