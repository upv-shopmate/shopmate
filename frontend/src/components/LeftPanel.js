/* eslint react/prop-types: 0 */

import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';
import UserList from './UserList';
import ListProduct from './ListProduct';

const DEFAULT_TITLE = 'Lista de la compra';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'buttonEnabled': false,
      'currentList': null,
      'inList': false,
      'title': DEFAULT_TITLE,
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

  handleListClick(list) {
    this.setState({
      currentList: list,
      inList: true,
      title: list.name,
    });
  }

  showLists() {
    this.setState({
      inList: false,
      title: DEFAULT_TITLE,
    });
  }
  renderList() {
    if (this.state.currentList !== null) {
      // FIXME
      // change this implementation for one that makes sense with the data
      // received in the endpoint from the backend
      return this.state.currentList.entries.map((product) =>
        <ListProduct
          key={product.id}
          name={product.name}
          quantity={1}
          image={product.pictures[0]}
        />,
      );
    }
  }
  renderLists() {
    if (this.props.lists) {
      return this.props.lists.map((list) =>
        <UserList
          key={list['name']}
          entries={list['entries']}
          name={list['name']}
          onListClick={(list) => this.handleListClick(list)}
          list={list}
        />,
      );
    } else {
      return (
        <div className="no-lists">No tiene listas creadas</div>
      );
    }
  }

  // FIXME dropdown funcionality should change this logic
  renderListPanel() {
    if (this.state.inList) {
      return (
        <div className="lf-lists">
          {this.renderList()}
        </div>
      );
    } else {
      return (
        <div className="lf-lists">
          {this.renderLists()}
        </div>
      );
    }
  }
  renderPanel() {
    if (this.state.userLoggedIn) {
      return this.renderListPanel();
    } else {
      return (
        <NotLoginPanel openLogin={this.openLoginPanel.bind(this)} />
      );
    }
  }

  render() {
    return (
      <div className="left-panel">
        <div className="left-panel-title">{this.state.title}</div>
        <div className="current-panel">
          {this.renderPanel()}
        </div>
        <div className="bottom-buttons">
          <button
            disabled={!this.state.buttonEnabled}
            className="lf-list-button"
            onClick={() => this.showLists()}
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
