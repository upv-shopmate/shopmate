/* eslint react/prop-types: 0 */

import '../assets/css/RightPanel.css';
import React from 'react';
import Catalog from './Catalog';
import Cart from './Cart';
import Map from './Map';
import Searcher from './SearchPanel';
import Square from './Square';
import {requestMap} from '../requests/MapRequest';

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

  async initializeMap() {
    const map = await requestMap();
    this.drawMap(map);
  }
  drawMap(map) {
    for (let x = 0; x < map.length; x++) {
      for (let y = 0; y < map[x].length; y++) {
        const right = map[x][y + 1];
        let down;
        if (x + 1 < map.length) {
          down = map[x + 1][y];
        }
        let needsShadow = false;
        if (right == 0 ||down == 0) needsShadow = true;
        map[x][y] = (
          <Square
            key={(x, y)}
            shadow={needsShadow}
            color={map[x][y]} x={x} y={y}
          />);
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
