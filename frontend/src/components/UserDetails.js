import signOutIcon from "../assets/images/sign_out_icon.png";
import leftArrow from "../assets/images/left_arrow.png";
import '../assets/css/UserDetails.css';
import React from 'react';

class UserDetails extends React.Component {

  render() {
    return (
      <div className="user-details-panel">
        <div className="user-details-title">Información de la cuenta</div>
        <div className="user-details">
          <div className="icon-user">M</div>
          <div className="right-block">
            <div className="user-name">Manolo Díaz</div>
            <button className="logout-button shadow">
              <img className="logout-button-image" src={signOutIcon}></img>
              <div className="logout-button-text">CERRAR SESIÓN</div>
            </button>
          </div>

        </div>
        <button className="user-details-return-button shadow">
          <img className="user-details-return-button-image" src={leftArrow}></img>
          <div className="user-details-return-button-text">VOLVER</div>
        </button>
      </div>
    );
  }
}

export default UserDetails;