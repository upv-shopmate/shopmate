/* eslint react/prop-types: 0 */

import '../assets/css/RightPanel.css';
import React from 'react';
import Catalog from './Catalog';
import Cart from './Cart';
import Map from './Map';
import Searcher from './SearchPanel';
import Square from './Square';
import {requestMap} from '../requests/MapRequest';
import {requestCatalog} from '../requests/ProductRequest.js';
import {requestCatalogByCategory} from '../requests/ProductRequest.js';
import loadingGif from '../assets/images/loading.gif';
import {getStore} from '../utils/Store';

// minimum width is 70
const WIDTHS = {
  CART: '60%',
  CATALOG: '270%',
  MAP: '250%',
  SEARCHER: '250%',
};
class RightPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'componentDidMount': false,
      'map': [],
      'catalog': [],
      'catalogPage': 0,
      'initialCatalog': [],
      'results': [],
      'completedSearch': false,
      'selectedCategory': undefined,
    };
    this.currentPanel = this.currentPanel.bind(this);
    this.catalogRef = React.createRef();
    this.loadingRef = React.createRef();
  }

  updateCategory(category) {
    this.setState({
      'selectedCategory': category,
      'catalogPage': 1,
    }, () => {
      this.updateCatalog(1);
    });
  }


  componentDidMount() {
    this.setState({
      'componentDidMount': true,
    });
    this.hideLoading();
    this.initializeMap();
    this.initializeCatalog();
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
  }


  async initializeCatalog() {
    let catalog;
    const store = getStore();
    try {
      catalog = await requestCatalog(0);
      store.showError(false);
      this.setState({
        'initialCatalog': catalog,
        'catalog': catalog,
        'catalogPage': 1,
      });
      if (this.catalogRef.current !== null) {
        this.catalogRef.current.updateCatalog(catalog);
      }
    } catch (e) {
      store.showError(true);
      this.initializeCatalog();
    }
  }

  async updateCatalog(page) {
    let catalog;
    const store = getStore();
    try {
      this.showLoading();
      if (this.state.selectedCategory == undefined) {
        catalog = await requestCatalog(page - 1);
      } else {
        catalog = await requestCatalogByCategory(this.state.selectedCategory.id, page - 1);
      }
      if (catalog != undefined) {
        this.setState({
          'catalog': catalog,
          'catalogPage': page,
        }, store.showError(false));
        if (this.catalogRef.current !== null) {
          this.catalogRef.current.updateCatalog(catalog);
        }
      }
      this.hideLoading();
    } catch (e) {
      console.error(e);
      this.hideLoading();
      store.showError(true);
      this.updateCatalog(page);
    }
  }

  resetCatalog() {
    this.setState({
      'catalog': this.state.initialCatalog,
      'catalogPage': 1,
    });
    if (this.catalogRef.current !== null) {
      this.catalogRef.current.updateCatalog(this.state.initialCatalog);
    }
  }

  async initializeMap() {
    let map;
    const store = getStore();
    try {
      map = await requestMap();
      this.drawMap(map);
      store.showError(false);
    } catch (e) {
      store.showError(true);
      this.initializeMap();
    }
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
        if (right == 0 || down == 0) needsShadow = true;
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

  updateSearchPanel(input) {
    this.setState({
      'results': input,
    });
  }

  changeCompletedSearchResultsPanel(input) {
    this.setState({
      'completedSearch': input,
    });
  }

  scrollToTopResultsPanel() {
    const searchResults = document.querySelector('.searcher-results');
    if (searchResults !== null) {
      searchResults.scrollTo(0, 0);
    }
  }
  searchMoreResults() {
    this.props.moreResults();
  }


  changePanel(input) {
    const panel = input;
    if (panel === 'cart') {
      this.changePanelWidth(WIDTHS.CART);
      return <Cart
        cartContent={this.props.cartContent}
        showErrorPanel={this.props.showErrorPanel}
        hideErrorPanel={this.props.hideErrorPanel}
        addProductToCartContent={this.props.addProductToCartContent}
        removeProductToCartContent={this.props.removeProductToCartContent}
        resetCartContent={this.props.resetCartContent}
      />;
    } else if (panel === 'catalog') {
      this.changePanelWidth(WIDTHS.CATALOG);
      return <Catalog
        updateCategory={this.updateCategory.bind(this)}
        catalog={this.state.catalog}
        ref={this.catalogRef}
        onGoToPage={(page) => this.updateCatalog(page)}
        page={this.state.catalogPage}
        zoomImage={this.props.zoomImage}
      />;
    } else if (panel === 'map') {
      this.changePanelWidth(WIDTHS.MAP);
      return <Map map={this.state.map} />;
    } else if (panel === 'searcher') {
      this.changePanelWidth(WIDTHS.SEARCHER);
      return (
        <Searcher
          completedSearch={this.state.completedSearch}
          goToLastState={this.props.goToLastState}
          onResultsBottomPage={() => this.searchMoreResults()}
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

  showLoading() {
    this.loadingRef.current.style.display = 'inherit';
  }
  hideLoading() {
    this.loadingRef.current.style.display = 'none';
  }

  render() {
    return (
      <div className="right-panel">
        <this.currentPanel />
        <img
          src={loadingGif}
          className="loading-gif"
          ref={this.loadingRef}></img>
      </div>
    );
  }
}

export default RightPanel;
