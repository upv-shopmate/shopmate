/* eslint react/prop-types: 0 */

import connectionGif from '../assets/images/no_connection.gif';
import React, {Component} from 'react';
import '../assets/css/ErrorPanel.css';
import {withTranslation} from 'react-i18next';

class ErrorPanel extends Component {
  constructor(props) {
    super(props);
  }

  closePanel() {
    this.props.closeErrorPanel();
  }

  render() {
    const {t} = this.props;
    return (
      <div className="ep-window">
        <div className="ep-panel">
          <div className="ep-title">
            {t('errorPanel.title')}
          </div>
          <div className="ep-subtitle">
            {t('errorPanel.message')}
          </div>
          <img className="ep-loading-gif" src={connectionGif}></img>
        </div>
      </div>
    );
  }
}

export default withTranslation()(ErrorPanel);
