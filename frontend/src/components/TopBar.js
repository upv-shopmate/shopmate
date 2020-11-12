/* eslint react/prop-types: 0 */
import '../assets/css/TopBar.css';
import React from 'react';
import logo from '../assets/images/logo.png';
import exitIcon from '../assets/images/leave_icon.png';
import translateIcon from '../assets/images/translate.png';
import SearchField from './SearchField';

// TODO: extract user field to a new component, DO NOT implement user here
class TopBar extends React.Component {
  constructor(props) {
    super(props);
  }


  render() {
    return (
      <div className="top-bar">
        <div className="left-side">
          <img src={logo}></img>
          <div className="logo-name">ShopMate</div>
        </div>
        <SearchField
          goToLastState={this.props.goToLastState}
          onChangeRightPanel={this.props.onChangeRightPanel}
          changeResults={this.props.changeResults}
        />
        <div className="right-side">
          <button className="user-field shadow">
            <div className="first-letter"></div>
            <div className="name">Iniciar sesión</div>
          </button>
          <button className="translate-button top-button shadow">
            <img src={translateIcon}></img>
          </button>
          <button className="exit-button top-button shadow">
            <img src={exitIcon}></img>
          </button>
        </div>
      </div>
    );
  }
}

export default TopBar;
