import '../assets/css/Login.css';
import Input from './Input';
import React from 'react';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      value: '',
    });
    this.updateSearchText = this.updateSearchText.bind(this);
  }

  updateSearchText(input) {
    this.setState({
      searchInput: input,
    });
  }

  render() {
    return (
      <div className="loginUser">
        <div className="login-block">
          <div className="login-title">
            <span className="init-title">Iniciando sesión en su cuenta de </span>
            <span className="brand-text">Shopmate</span>
          </div>
          <div className="user-inputs">
            <div className="login-field">
              <Input
                type="text"
                className="username-field"
                placeholder="Usuario"
                onChangeParent={this.updateSearchText}
              />
            </div>
            <div className="login-field">
              <Input
                type="password"
                className="password-field"
                placeholder="Contraseña"
                onChangeParent={this.updateSearchText}
              />
            </div>
          </div>
          <div className="bottom-block">
            <div className="info">Pulse sobre las cajas de texto para mostrar el teclado en pantalla.</div>
            <button className="accept-login-button">INICIAR SESIÓN</button>
          </div>
        </div>
        <div className="problem-question">¿Tiene problemas para iniciar sesión?</div>
        <div className="solution">Pregunte a un empleado o acuda al servicio de atención al cliente más cercano</div>
        <button className="return-button">VOLVER</button>
      </div>
    );
  }
}

export default Login;
