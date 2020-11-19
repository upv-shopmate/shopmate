/* eslint react/prop-types: 0 */

import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';
import UserLists from './UserLists';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'buttonEnabled': false,
    };
  }

  componentDidMount() {
    this.initizializePanel();
  }

  initizializePanel() {
    if (this.props.userLoggedIn) {
      this.logIn();
    } else {
      this.logOut();
    }
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  enableListsButton() {
    const button = document.querySelector('.lf-list-button');
    button.style.opacity = 1;
    this.setState({
      'buttonEnabled': true,
    });
  }

  disableListsButton() {
    const button = document.querySelector('.lf-list-button');
    button.style.opacity = 0.4;
    this.setState({
      'buttonEnabled': false,
    });
  }

  logIn() {
    this.enableListsButton();
    this.setState({
      'userLoggedIn': true,
    });
  }

  logOut() {
    this.disableListsButton();
    this.setState({
      'userLoggedIn': false,
    });
  }

  renderPanel() {
    if (this.state.userLoggedIn) {
      return (
        <React.Fragment>
          <UserLists/>
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
    return (
      <div className="left-panel">
        <div className="left-panel-title">Lista de la compra</div>
        <div className="current-panel">
          {this.renderPanel()}
        </div>
        <div className="bottom-buttons">
          <button
            disabled={!this.state.buttonEnabled}
            className="lf-list-button"
          >
            <img className="list-button-image" src={listImage}></img>
            <div className="list-button-text">MIS LISTAS</div>
          </button>
          <button
            className="lf-tag-button"
          >
            <img className="tag-button-image" src={tagImage}></img>
            <div className="tag-button-text">CUPONES</div>
          </button>
        </div>
      </div>
    );
  }
}

export default LeftPanel;
