/* eslint react/prop-types: 0 */

import '../assets/css/RightPanel.css';
import React from 'react';
import Catalog from './Catalog';
import Cart from './Cart';
import Map from './Map';
import Searcher from './SearchPanel';

//minimum width is 70
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
      'componentDidMount': false
    };
    this.currentPanel = this.currentPanel.bind(this);
  }

  componentDidMount() {
    this.setState({
      'componentDidMount': true,
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
      return <Map />;
    } else if (panel === 'searcher') {
      this.changePanelWidth(WIDTHS.SEARCHER);
      return <Searcher goToLastState={this.props.goToLastState} results={this.props.results} />;
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
