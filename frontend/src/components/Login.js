/* eslint react/prop-types: 0 */

import '../assets/css/Login.css';
import passwordImage from '../assets/images/password_lock.png';
import accountImage from '../assets/images/account_circle.png';
import infoImage from '../assets/images/info.png';
import rightArrow from '../assets/images/right_arrow.png';
import leftArrow from '../assets/images/left_arrow.png';
import {userAuthRequest} from '../requests/UserRequests.js';
import Input from './Input';
import React from 'react';
import {withTranslation} from 'react-i18next';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      account: '',
      password: '',
      errorMessage: '',
    });
    this.updateAccountText = this.updateAccountText.bind(this);
    this.updatePasswordText = this.updatePasswordText.bind(this);
    this.closeLoginPanel = this.closeLoginPanel.bind(this);
    this.login = this.login.bind(this);
  }

  updateAccountText(input) {
    this.setState({
      account: input,
    });
    this.changePasswordError('');
  }

  updatePasswordText(input) {
    this.setState({
      password: input,
    });
    this.changePasswordError('');
  }


  changePasswordError(error) {
    this.setState({
      errorMessage: error,
    });
  }

  closeLoginPanel() {
    this.props.closeLogin();
  }

  async login() {
    const {t} = this.props;
    const account = this.state.account;
    const password = this.state.password;
    const response = await userAuthRequest(account, password);
    if (response.status == 200) {
      this.props.loginUser(response.accessToken);
      this.closeLoginPanel();
      this.props.hideErrorPanel();
    } else if (response.status == 401) {
      this.changePasswordError(t('login.incorrectEmailPass'));
      this.props.hideErrorPanel();
    } else if (response.status === 'ConnectionError') {
      this.props.showErrorPanel();
      this.login();
    }
  }

  render() {
    const {t} = this.props;
    return (
      <div className="loginUser">
        <div className="login-block shadow-box-users">
          <div className="login-title">
            <span className="init-title">
              {t('login.logingIn')}
            </span>
            <span className="brand-text">Shopmate</span>
          </div>
          <div className="user-inputs">
            <div className="login-field shadow-box-users ">
              <img
                className="account-field-image"
                src={accountImage}></img>
              <Input
                type="text"
                placeholder= {t('login.email')}
                onChangeParent={this.updateAccountText}
              />
            </div>
            <div className="login-field shadow-box-users ">
              <img
                className="password-image"
                src={passwordImage}></img>
              <Input
                type="password"
                placeholder= {t('login.password')}
                onChangeParent={this.updatePasswordText}
              />
            </div>
            <div className="login-error">{this.state.errorMessage}</div>
          </div>
          <div className="bottom-block">
            <div className="info-block">
              <img
                className="info-icon"
                src={infoImage}/>
              <div className="info-text">{t('login.loginInfoText')}</div>
            </div>
            <button
              className="accept-login-button shadow-box-users "
              onClick={this.login}>
              <div className="login-button-text">{t('login.login')}</div>
              <img className="login-button-image" src={rightArrow}></img>
            </button>
          </div>
        </div>
        <div className="problem-question">{t('login.loginQuestion')}</div>
        <div className="solution">{t('login.loginSolution')}</div>
        <div className="login-return-button-wrapper">
          <button
            className="login-return-button shadow-box-users "
            onClick={this.closeLoginPanel}
          >
            <img className="login-return-button-image" src={leftArrow}></img>
            <div className="login-return-button-text">{t('login.return')}</div>
          </button>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Login);
