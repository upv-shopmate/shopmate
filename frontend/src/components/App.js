import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';
import { requestSearchDataBase } from '../requests/SearchRequests.js';

export const dataBaseURL = 'https://localhost:5001';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedPanel': 'cart',
      'results': [],
      'lastPanel': '',
      'resultsPage': 0,
      'lastSearchInput': "",
      'noMoreResults': false,
    };
    this.changeProductResults = this.changeProductResults.bind(this);
    this.goToLastState = this.goToLastState.bind(this);
    this.rightPanelRef = React.createRef();
  }

  getAndChangeResultsPage() {
    const page = this.state.resultsPage;
    this.setState({
      'resultsPage': page + 1,
    })
    return page + 1;
  }

  resetResults() {
    this.setState({
      'resultsPage': 0,
      'results': [],
    })
  }

  changeLastPanel(panel) {
    this.setState({
      lastPanel: panel,
    });
  }

  goToLastState() {
    this.setState({
      'selectedPanel': this.state.lastPanel,
    });
    this.resetResults();
  }

  changeRightPanel(panel) {
    this.setState({
      'selectedPanel': panel,
    });
  }

  changeProductResults(searchInput) {
    let page = 0;
    let lastSearchInput = this.state.lastSearchInput;
    let noMoreResults = this.state.noMoreResults;


    if (searchInput === undefined || searchInput === lastSearchInput) {
      page = this.getAndChangeResultsPage();
      searchInput = lastSearchInput;
    } else if (lastSearchInput !== searchInput) {
      this.resetResults();
      this.setState({
        'lastSearchInput': searchInput,
        'noMoreResults': false,
      })
      noMoreResults = false;
    } else {
      return 0;
    }
    if (!noMoreResults) {
      this.requestAndUpdateResults(searchInput, lastSearchInput, page);
    }
  }

  async requestAndUpdateResults(searchInput, lastSearchInput, page) {
    let rightPanelRef = this.rightPanelRef.current;
    rightPanelRef.showLoading();
    const products = await requestSearchDataBase(searchInput, page);
    if (searchInput !== lastSearchInput) rightPanelRef.scrollToTopResultsPanel();
    if (products.length == 0) {
      rightPanelRef.hideLoading();
      this.setState({
        'noMoreResults': true
      })
    } else {
      const results = this.state.results.concat(products);
      this.setState({
        'results': results,
      });
      rightPanelRef.updateSearchPanel(this.state.results);

      rightPanelRef.hideLoading();
    }
  }

  render() {
    return (
      <div className="app">
        <TopBar
          onChangeRightPanel={this.changeRightPanel.bind(this)}
          changeResults={this.changeProductResults}
          goToLastState={this.goToLastState}
        />
        <div className="panels">
          <LeftPanel />
          <RightPanel
            moreResults={this.changeProductResults}
            ref={this.rightPanelRef}
            panel={this.state.selectedPanel}
            goToLastState={this.goToLastState}
            results={this.state.results}
          />
        </div>
        <Nav
          changeLastPanel={this.changeLastPanel.bind(this)}
          onChangeRightPanel={this.changeRightPanel.bind(this)}
        />
      </div>
    );
  }
}

export default App;
