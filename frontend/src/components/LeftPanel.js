/* eslint react/prop-types: 0 */

import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';
import {withTranslation} from 'react-i18next';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  renderPanel() {
    if (this.props.userLoggedIn) {
      return (
        <React.Fragment>

        </React.Fragment>
      );
    } else {
      return (
        <React.Fragment>
          <NotLoginPanel openLogin={this.openLoginPanel.bind(this)} />
        </React.Fragment>
      );
    }
  }

  render() {
    const {t, i18n} = this.props;
    return (
      <div className="left-panel">
        <div className="left-panel-title">{t('shoppingList')}</div>
        <div className="current-panel">
          {this.renderPanel()}
        </div>
        <div className="bottom-buttons">
          <button
            disabled={!this.props.buttonEnabled}
            className="lf-list-button"
          >
            <img className="list-button-image" src={listImage}></img>
            <div className="list-button-text">{t('myLists')}</div>
          </button>
          <button
            className="lf-tag-button"
          >
            <img className="tag-button-image" src={tagImage}></img>
            <div className="tag-button-text">{t('coupons')}</div>
          </button>
        </div>
      </div>
    );
  }
}

export default withTranslation()(LeftPanel);
