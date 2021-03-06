/* eslint react/prop-types: 0 */

import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';
import {withTranslation} from 'react-i18next';
import UserList from './UserList';
import ListProduct from './ListProduct';
import {getStore} from '../utils/Store';
import CouponsList from './CouponsList';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'inList': false,
      'showingLists': true,
      'showingCoupons': false,
    };
    this.showLists = this.showLists.bind(this);
    this.showCoupons = this.showCoupons.bind(this);
  }

  componentDidMount() {
    this.initizializeButtons();
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
  }

  initizializeButtons() {
    if (this.props.userLoggedIn) {
      this.props.enableListsButton();
    } else {
      this.props.disableListsButton();
    }
  }

  openLoginPanel() {
    this.props.openLogin();
  }

  showLists() {
    const store = getStore();
    store.dispatch({
      type: 'changeCurrentList',
      currentList: null,
    });
    this.setState({
      inList: false,
      showingLists: true,
      showingCoupons: false,
    });
  }

  showCoupons() {
    this.setState({
      showingLists: false,
      showingCoupons: true,
    });
  }

  renderList() {
    const store = getStore();
    const currentList = store.getState().currentList;
    if (currentList) {
      return currentList.entries.map((product) =>
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
    const {t} = this.props;
    if (this.props.lists) {
      return this.props.lists.map((list) =>
        <UserList
          key={list['name']}
          entries={list['entries']}
          name={list['name']}
          list={list}
        />,
      );
    } else {
      return (
        <div className="no-lists">{t('leftPanel.noListMessage')}</div>
      );
    }
  }

  renderListPanel() {
    const store = getStore();
    const currentList = store.getState().currentList;
    if (currentList !== null) {
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

  renderCouponsPanel() {
    return (
      <div className="lf-lists">
        <CouponsList
          coupons={this.props.coupons}
          appliedCoupons={this.props.appliedCoupons}
          applyCoupon={this.props.applyCoupon}
          removeCoupon={this.props.removeCoupon}
        ></CouponsList>
      </div>
    );
  }

  renderPanel() {
    if (this.props.userLoggedIn) {
      if (this.state.showingLists) {
        return this.renderListPanel();
      } else if (this.state.showingCoupons) {
        return this.renderCouponsPanel();
      }
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
        <div className="left-panel-title">{t('leftPanel.shoppingList')}</div>
        <div className="current-panel">
          {this.renderPanel()}
        </div>
        <div className="bottom-buttons">
          <button
            disabled={!this.props.buttonEnabled}
            className="lf-list-button"
            onClick={this.showLists}
          >
            <img className="list-button-image" src={listImage}></img>
            <div className="list-button-text">{t('leftPanel.myLists')}</div>
          </button>
          <button
            disabled={!this.props.buttonEnabled}
            className="lf-tag-button"
            onClick={this.showCoupons}
          >
            <img className="tag-button-image" src={tagImage}></img>
            <div className="tag-button-text">{t('leftPanel.coupons')}</div>
          </button>
        </div>
      </div>
    );
  }
}

export default withTranslation()(LeftPanel);
