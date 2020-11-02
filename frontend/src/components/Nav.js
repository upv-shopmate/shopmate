/* eslint react/prop-types: 0 */

import '../assets/css/Nav.css';
import React from 'react';
import shoppingIcon from '../assets/images/shopping_icon.png';
import mapIcon from '../assets/images/map_icon.png';
import storeIcon from '../assets/images/store_icon.png';

const SELECTED_BUTTON_COLOR = '#FDA332';
const UNSELECTED_BUTTON_COLOR = 'grey';

class Nav extends React.Component {
  componentDidMount() {
    this.initializeRefs();
    this.initializeButtonBackground();
  }

  initializeRefs() {
    this.cartButton = React.createRef();
    this.catalogButton = React.createRef();
    this.mapButton = React.createRef();
  }

  initializeButtonBackground() {
    this.unselectEveryButton();
    this.changePanel('cart');
  }

  unselectEveryButton() {
    const buttons = document.querySelectorAll('.nav-button');
    buttons.forEach((element) => {
      element.style.backgroundColor = UNSELECTED_BUTTON_COLOR;
    });
  }

  changePanel(panel) {
    this.props.onChangeRightPanel(panel);
    this.props.changeLastPanel(panel);
    this.changeSelectedButton(panel);
  }

  changeSelectedButton(panel) {
    this.unselectEveryButton();
    const navButton = document.querySelector('#' + panel);
    navButton.style.backgroundColor = SELECTED_BUTTON_COLOR;
  }

  render() {
    return (
      <div className="nav">
        <button className="nav-button" onClick={() => {
          this.changePanel('cart');
        }} id="cart">
          <img src={shoppingIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">CARRITO</div>
        </button>
        <button className="nav-button" onClick={() => {
          this.changePanel('catalog');
        }} id="catalog">
          <img src={storeIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">CATÁLOGO</div>
        </button>
        <button className="nav-button" onClick={() => {
          this.changePanel('map');
        }} id="map">
          <img src={mapIcon} className="nav-button-icon"></img>
          <div className="nav-button-name">MAPA</div>
        </button>
      </div>
    );
  }
}

export default Nav;
