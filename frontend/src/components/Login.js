import React from 'react';

class Login extends React.Component {

    render() {
        return (
          <div className="loginUser">
              <div className="login-block">
                <div className="login-title">Iniciando sesión en su cuenta de Shopmate</div>
                <div className="username-field"></div>
                <div className="password-field"></div>
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
