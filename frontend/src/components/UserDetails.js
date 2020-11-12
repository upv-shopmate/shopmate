import React from 'react';

class UserDetails extends React.Component {

    render() {
        return (
          <div className="user-details-panel">
              <div className="details-title">Información de la cuenta</div>
              <div className="user-details">
                <div className="icon-user">M</div>
                <div className="user-name">Manolo Díaz</div>
              </div>
              <button className="logout">CERRAR SESIÓN</button>
              <button className="return-button">VOLVER</button>
          </div>
        );
    }
}

export default UserDetails;