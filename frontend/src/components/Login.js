import '../assets/css/Login.css';
import passwordImage from "../assets/images/password_lock.png"
import accountImage from "../assets/images/account_circle.png"
import infoImage from "../assets/images/info.png"
import rightArrow from "../assets/images/right_arrow.png"
import leftArrow from "../assets/images/left_arrow.png"
import { userAuthRequest } from "../requests/UserRequests.js"
import Input from './Input';
import React from 'react';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      account: '',
      password: '',
      accountError: '',
      passwordError: '',
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
  }

  updatePasswordText(input) {
    this.setState({
      password: input,
    });
  }

  changeAccountError(error) {
    this.setState({
      accountError: error,
    });
  }

  changePasswordError(error) {
    this.setState({
      passwordError: error,
    });
  }

  closeLoginPanel() {
    this.props.closeLogin();
  }

  async login() {
    let response = await userAuthRequest(this.state.account, this.state.password);
    if (response.status == 200) {
      this.props.loginUser(response.accesToken);
      this.closeLoginPanel();
    } else {
      this.changePasswordError("Correo o contraseña incorrectos");
    }
  }

  render() {
    return (
      <div className="loginUser">
        <div className="login-block shadow-box-users ">
          <div className="login-title">
            <span className="init-title">Iniciando sesión en su cuenta de </span>
            <span className="brand-text">Shopmate</span>
          </div>
          <div className="user-inputs">
            <div className="login-field shadow-box-users ">
              <img
                className="account-field-image"
                src={accountImage}></img>
              <Input
                type="text"
                placeholder="Correo electrónico"
                onChangeParent={this.updateAccountText}
              />
            </div>
            <div className="login-error">{this.state.accountError}</div>
            <div className="login-field shadow-box-users ">
              <img
                className="password-image"
                src={passwordImage}></img>
              <Input
                type="password"
                placeholder="Contraseña"
                onChangeParent={this.updatePasswordText}
              />
            </div>
            <div className="login-error">{this.state.passwordError}</div>
          </div>
          <div className="bottom-block">
            <div className="info-block">
              <img
                className="info-icon"
                src={infoImage}></img>
              <div className="info-text">Pulse sobre las cajas de texto para mostrar el teclado en pantalla.</div>
            </div>
            <button
              className="accept-login-button shadow-box-users "
              onClick={this.login}>
              <div className="login-button-text">INICIAR SESIÓN</div>
              <img className="login-button-image" src={rightArrow}></img>
            </button>
          </div>
        </div>
        <div className="problem-question">¿Tiene problemas para iniciar sesión?</div>
        <div className="solution">Pregunte a un empleado o acuda al servicio de atención al cliente más cercano.</div>
        <div className="login-return-button-wrapper">
          <button
            className="login-return-button shadow-box-users "
            onClick={this.closeLoginPanel}
          >
            <img className="login-return-button-image" src={leftArrow}></img>
            <div className="login-return-button-text">VOLVER</div>
          </button>
        </div>
      </div>
    );
  }
}

export default Login;
