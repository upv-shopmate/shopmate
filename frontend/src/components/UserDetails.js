/* eslint react/prop-types: 0 */

import signOutIcon from '../assets/images/sign_out_icon.png';
import leftArrow from '../assets/images/left_arrow.png';
import '../assets/css/UserDetails.css';
import React from 'react';
import {withTranslation} from 'react-i18next';

class UserDetails extends React.Component {
  constructor(props) {
    super(props);
  }

  closeUserDetails() {
    this.props.closeUserDetails();
  }

  closeAndLogOut() {
    this.props.logOut();
  }

  getName() {
    return this.props.user.name;
  }

  getFirstLetter() {
    const name = this.props.user.name;
    return name.charAt(0).toUpperCase();
  }

  getEmail() {
    return this.props.user.email;
  }

  getPhoneNumber() {
    return this.props.user.phone;
  }

  render() {
    const {t} = this.props;
    return (
      <div className="user-details-panel">
        <div className="user-details-title">{t('userDetails.userDetailsTitle')}</div>
        <div className="user-details">
          <div className="icon-user">{this.getFirstLetter()}</div>
          <div className="right-block">
            <div className="user-name">{this.getName()}</div>
            <div className="ud-more-info">
              <div className="ud-email">{this.getEmail()}</div>
              <div className="ud-phone">{this.getPhoneNumber()}</div>
            </div>
            <button
              className="logout-button shadow"
              onClick={this.closeAndLogOut.bind(this)}>
              <img className="logout-button-image" src={signOutIcon}></img>
              <div className="logout-button-text">{t('userDetails.logout')}</div>
            </button>
          </div>

        </div>
        <button
          className="user-details-return-button shadow"
          onClick={this.closeUserDetails.bind(this)}>
          <img
            className="user-details-return-button-image"
            src={leftArrow}></img>
          <div className="user-details-return-button-text">{t('userDetails.return')}</div>
        </button>
      </div>
    );
  }
}

export default withTranslation()(UserDetails);
