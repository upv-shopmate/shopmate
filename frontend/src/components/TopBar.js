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
    this.state = {
      'userLoggedIn': false,
      'userInfo': undefined,
    };
  }

  logIn(user) {
    this.setState({
      'userLoggedIn': true,
      'userInfo': user,
    });
  }

  logOut() {
    this.setState({
      'userLoggedIn': false,
      'userInfo': undefined,
    });
  }

  getUserName() {
    let name = this.state.userInfo.name;
    const splitName = name.split(' ');
    name = splitName[0];
    if (splitName.length > 2) name += ' ' + splitName[1].charAt(0).toUpperCase() + '.';
    return name;
  }

  getUserFirstLetter() {
    const name = this.state.userInfo.name;
    return name.charAt(0).toUpperCase();
  }


  renderButtons() {
    if (this.state.userLoggedIn) {
      return (
        <React.Fragment>
          <button
            className="user-button shadow"
            onClick={this.props.openUserDetails}>
            <div className="user-first-letter">{this.getUserFirstLetter()}</div>
            <div className="user-button-name">{this.getUserName()}</div>
          </button>
          <button className="translate-button top-button shadow">
            <img src={translateIcon}></img>
          </button>
          <button className="exit-button top-button shadow"
            onClick={this.props.logOut}
          >
            <img src={exitIcon}></img>
          </button>
        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <button className="user-login-button shadow"
            onClick={this.props.openLogin} >Iniciar sesión</button>
          <button className="translate-button top-button shadow">
            <img src={translateIcon}></img>
          </button>
        </React.Fragment>
      );
    }
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
          {this.renderButtons()}
        </div>
      </div>
    );
  }
}

export default TopBar;
