/* eslint react/prop-types: 0 */

import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';
<<<<<<<<< Temporary merge branch 1
import UserList from './UserList';
=========
import {withTranslation} from 'react-i18next';
import UserList from './UserList';
import ListProduct from './ListProduct';
import Cart from './Cart';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'currentList': null,
      'inList': false,
    };
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  handleListClick(list) {
    this.setState({
      currentList: list,
      inList: true,
    });
  }

  showLists() {
    this.setState({
        inList: false,
    });
  }

  renderList() {
    if (this.state.currentList !== null) {
      // FIXME
      // change this implementation for one that makes sense with the data
      // received in the endpoint from the backend
      this.props.onGetCurrentList(this.state.currentList);
      return this.state.currentList.entries.map((product) =>
        <ListProduct
          key={product.item.id}
          name={product.item.name}
          quantity={product.quantity}
          image={product.item.pictures[0]}
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
    if (this.props.userLoggedIn) {
      return this.renderListPanel();
    } else {
      return (
        <NotLoginPanel openLogin={this.openLoginPanel.bind(this)} />
      );
    }
  }

  render() {
    const {t} = this.props;
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
            onClick={() => this.showLists()}
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
