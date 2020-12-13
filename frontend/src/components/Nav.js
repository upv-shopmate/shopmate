/* eslint react/prop-types: 0 */

import '../assets/css/Nav.css';
import React from 'react';
import shoppingIcon from '../assets/images/shopping_icon.png';
import mapIcon from '../assets/images/map_icon.png';
import storeIcon from '../assets/images/store_icon.png';
import {withTranslation} from 'react-i18next';
import {getStore} from '../utils/Store.js';

const SELECTED_BUTTON_COLOR = '#FDA332';
const UNSELECTED_BUTTON_COLOR = '#393e46';

class Nav extends React.Component {
  componentDidMount() {
    this.initializeRefs();
    this.initializeButtonBackground();
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
  }

  componentDidUpdate() {
    this.changeSelectedButton(getStore().getState().panel);
  }

  initializeRefs() {
    this.cartButton = React.createRef();
    this.catalogButton = React.createRef();
    this.mapButton = React.createRef();
  }

  initializeButtonBackground() {
    this.unselectEveryButton();
    this.changePanel('cart', false);
  }

  unselectEveryButton() {
    const buttons = document.querySelectorAll('.nav-button');
    buttons.forEach((element) => {
      element.style.backgroundColor = UNSELECTED_BUTTON_COLOR;
    });
  }

  changePanel(panel, initialized) {
    this.props.changeLastPanel(panel);
    if (initialized) {
      getStore().changePanel(panel);
    }
    if (panel === 'catalog') this.props.resetCatalog();
  }

  changeSelectedButton(panel) {
    if (panel == 'searcher') return;
    this.unselectEveryButton();
    const navButton = document.querySelector('#' + panel);
    navButton.style.backgroundColor = SELECTED_BUTTON_COLOR;
  }

  render() {
    const {t} = this.props;
    return (
      <div className="nav">
        <button className="nav-button" onClick={() => {
          this.changePanel('cart', true);
        }} id="cart">
          <img src={shoppingIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">{t('cart')}</div>
        </button>
        <button className="nav-button" onClick={() => {
          this.changePanel('catalog', true);
        }} id="catalog">
          <img src={storeIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">{t('catalog')}</div>
        </button>
        <button className="nav-button" onClick={() => {
          this.changePanel('map', true);
        }} id="map">
          <img src={mapIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">{t('map')}</div>
        </button>
      </div>
    );
  }
}

export default withTranslation()(Nav);
