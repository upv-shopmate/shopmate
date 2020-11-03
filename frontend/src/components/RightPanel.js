/* eslint react/prop-types: 0 */

import '../assets/css/RightPanel.css';
import React from 'react';
import Catalog from './Catalog';
import Cart from './Cart';
import Map from './Map';
import Searcher from './SearchPanel';
import Square from './Square'

// minimum width is 70
const WIDTHS = {
  CART: '70%',
  CATALOG: '150%',
  MAP: '150%',
  SEARCHER: '150%',
};
class RightPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'componentDidMount': false,
      'map': [],
    };
    this.currentPanel = this.currentPanel.bind(this);
  }

  componentDidMount() {
    this.setState({
      'componentDidMount': true,
    });
    this.initializeMap();
  }

  /**
   * We compute squares before calling map
   * 1 negro(no racismo) -> fondo
   * 0 blanco -> espacios
   * 2 gris -> cajas(pagar)
   * 3 azul -> pescao
   * 4 rojo -> charcuteria
   * 5 verde -> verduras
   * 6 marrón -> el resto
   */
  initializeMap() {
    let squares = [],
      width =  Math.floor(853 / 10),
      height = Math.floor(566 / 10);
    for(let x = 0; x < height; x++) {
      squares[x] = []
      for(let y = 0; y < width; y++) {
        squares[x][y] = 0;
      }
    }
    //cajas
    squares = this.drawRectangle(squares, 45, 10, 56, 15, 2);
    squares = this.drawRectangle(squares, 45, 16, 56, 21, 2);
    squares = this.drawRectangle(squares, 45, 22, 56, 27, 2);

    //izq
    squares = this.drawRectangle(squares, 5, 0, 40, 3, 4);
    squares = this.drawRectangle(squares, 0, 0, 5, 15, 4);
    squares = this.drawRectangle(squares, 0, 15, 5, 30, 6);
    squares = this.drawRectangle(squares, 10, 10, 40, 15, 4);
    squares = this.drawRectangle(squares, 10, 20, 40, 25, 6);
    //der
    squares = this.drawRectangle(squares, 30, 30, 56, 85, 1);
    squares = this.drawRectangle(squares, 20, 35, 25, 80, 5);
    squares = this.drawRectangle(squares, 10, 35, 15, 80, 6);
    squares = this.drawRectangle(squares, 2, 65, 7, 80, 3);
    squares = this.drawRectangle(squares, 2, 45, 7, 60, 3);

    this.drawMap(squares)
  }
  drawRectangle(squares, x_ini, y_ini, x_fi, y_fi, color) {
    for(let i = x_ini; i < x_fi; i++) {
      for(let j = y_ini; j < y_fi; j++) {
        squares[i][j] = color;
      } 
    }
    return squares;
  }
  drawMap(map) {
    for(let x = 0; x < map.length; x++) {
      for(let y = 0; y < map[x].length; y++) {
        let right = map[x][y + 1];
        let down;
        if(x + 1 < map.length) {
          down = map[x + 1][y];
        }
        let needs_shadow = false;
        if(right == 0 || down == 0) needs_shadow = true; 
        map[x][y] = <Square shadow={needs_shadow} color={map[x][y]} x={x} y={y}/>;
      }
    }
    this.setState({
      'map': map,
    });
  }

  currentPanel() {
    return this.changePanel(this.props.panel);
  }

  changePanel(input) {
    const panel = input;
    if (panel === 'cart') {
      this.changePanelWidth(WIDTHS.CART);
      return <Cart />;
    } else if (panel === 'catalog') {
      this.changePanelWidth(WIDTHS.CATALOG);
      return <Catalog />;
    } else if (panel === 'map') {
      this.changePanelWidth(WIDTHS.MAP);
      return <Map map={this.state.map}/>;
    } else if (panel === 'searcher') {
      this.changePanelWidth(WIDTHS.SEARCHER);
      return (
        <Searcher
          goToLastState={this.props.goToLastState}
          results={this.props.results}
        />
      );
    }
  }


  changePanelWidth(width) {
    if (this.state.componentDidMount) {
      const rightPanel = document.querySelector('.right-panel');
      rightPanel.style.width = width;
    }
  }

  render() {
    return (
      <div className="right-panel">
        <this.currentPanel panel={this.props.panel} />
      </div>
    );
  }
}

export default RightPanel;
