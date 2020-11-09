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
    return page;
  }

  resetResults() {
    this.setState({
      'resultsPage': 0,
      'results': [],
    })
    console.log(this.state.resultsPage);
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

  async changeProductResults(searchInput) {
    if (this.state.lastSearchInput !== searchInput) this.resetResults()
    this.setState({
      lastSearchInput: searchInput,
    })
    this.rightPanelRef.current.showLoading();
    const resultsPage = this.getAndChangeResultsPage();
    const products = await requestSearchDataBase(searchInput, resultsPage);
    console.log(products);
    const results = products.concat(this.state.results);
    this.setState({
      'results': results,
    });
    console.log(this.state.results);
    this.rightPanelRef.current.updateSearchPanel(this.state.results);
    this.rightPanelRef.current.hideLoading();
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
