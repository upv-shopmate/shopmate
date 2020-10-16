
import '../assets/css/Nav.css';
import React from 'react';
import shopping_icon from '../assets/images/shopping_icon.png';
import map_icon from '../assets/images/map_icon.png';
import store_icon from '../assets/images/store_icon.png';

class Nav extends React.Component {
  changeWindow(window){
    this.props.onChangeRightPanel(window);
  }

  render() {
    return (
        <div class="nav">
          <button class="button" onClick={() => {this.changeWindow("cart")}}>
            <img src={shopping_icon} class="button-icon"></img>
            <div class="button-name">CARRITO</div>
          </button>
          <button class="button" onClick={() => {this.changeWindow("catalog")}}>
            <img src={store_icon} class="button-icon"></img>
            <div class="button-name">CATÁLOGO</div>
          </button>
          <button class="button" onClick={() => {this.changeWindow("map")}}>
            <img src={map_icon} class="button-icon"></img>
            <div class="button-name">MAPA</div>
          </button>
        </div>
    );
  }
}

export default Nav;