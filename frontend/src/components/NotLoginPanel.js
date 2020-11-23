/* eslint react/prop-types: 0 */

import '../assets/css/NotLoginPanel.css';
import accountImage from '../assets/images/account_circle.png';
import React from 'react';
import { withTranslation } from 'react-i18next';

class NotLoginPanel extends React.Component {
  constructor(props) {
    super(props);
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  render() {
    const { t, i18n } = this.props;
    return (
      <div>
        <div className="not-logged-user-block">
          <img className="account-image" src={accountImage}></img>
          <div className="question"> {t('alreadyHasAccount')} </div>
          <div className="login-text">{t('loginText')}</div>
          <button
            className="left-panel-login-button"
            onClick={this.openLoginPanel.bind(this)}
    >{t('login')}</button>
        </div>
      </div>
    );
  }
}

export default withTranslation()(NotLoginPanel);
