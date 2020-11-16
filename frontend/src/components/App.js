import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';
import { requestSearchDataBase } from '../requests/SearchRequests.js';
import Login from './Login';
import { userInfoRequest } from "../requests/UserRequests.js"
import UserDetails from './UserDetails';

export const dataBaseURL = 'https://localhost:5001';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedPanel': 'cart',
      'results': [],
      'lastPanel': '',
      'resultsPage': 0,
      'lastSearchInput': '',
      'noMoreResults': false,
      'login': false,
      'user': undefined,
      'accessToken': undefined,
    };
    this.changeProductResults = this.changeProductResults.bind(this);
    this.goToLastState = this.goToLastState.bind(this);
    this.rightPanelRef = React.createRef();
  }

  async getUserInfo(accessToken) {
    this.setState({
      accessToken: accessToken
    })
    let response = await userInfoRequest(accessToken);
    if (response.status == 200) this.setState({ user: response.data })
  }

  getAndChangeResultsPage() {
    const page = this.state.resultsPage;
    this.setState({
      'resultsPage': page + 1,
    });
    return page + 1;
  }

  resetCatalog() {
    if (this.rightPanelRef !== undefined) {
      this.rightPanelRef.current.resetCatalog();
    }
  }

  resetResults() {
    this.setState({
      'resultsPage': 0,
      'results': [],
    });
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

  userLogOut() {
    this.setState({
      'user': undefined,
      'accessToken': undefined
    });
  }

  changeProductResults(searchInput) {
    let page = 0;
    const lastSearchInput = this.state.lastSearchInput;
    let noMoreResults = this.state.noMoreResults;

    if (searchInput === undefined || searchInput === lastSearchInput) {
      page = this.getAndChangeResultsPage();
      searchInput = lastSearchInput;
    } else if (lastSearchInput !== searchInput) {
      this.resetResults();
      this.setState({
        'lastSearchInput': searchInput,
        'noMoreResults': false,
      });
      noMoreResults = false;
    }

    if (!noMoreResults) {
      this.requestAndUpdateResults(searchInput, lastSearchInput, page);
    }
  }

  async requestAndUpdateResults(searchInput, lastSearchInput, page) {
    const rightPanelRef = this.rightPanelRef.current;
    rightPanelRef.showLoading();
    if (searchInput !== lastSearchInput) {
      rightPanelRef.changeCompletedSearchResultsPanel(false);
      rightPanelRef.scrollToTopResultsPanel();
      rightPanelRef.updateSearchPanel([]);
    }
    const result = await requestSearchDataBase(searchInput, page);
    rightPanelRef.changeCompletedSearchResultsPanel(true);
    if (result.nextPage == null) {
      this.setState({
        'noMoreResults': true,
      });
    }

    const results = this.state.results.concat(result.items);
    this.setState({
      'results': results,
    });
    rightPanelRef.updateSearchPanel(this.state.results);

    rightPanelRef.hideLoading();
  }

  renderLeftPanel() {
    if (this.state.login) {
      return (
        <React.Fragment>
          <Login
            loginUser={this.getUserInfo.bind(this)}
            closeLogin={this.closeLoginPanel.bind(this)}
          />
        </React.Fragment>
      )
    } else {
      return (
        <React.Fragment>
          <LeftPanel
            openLogin={this.openLoginPanel.bind(this)}
          />
          <RightPanel
            panel={this.state.selectedPanel}
            goToLastState={this.goToLastState}
            results={this.state.results}
            moreResults={this.changeProductResults}
            ref={this.rightPanelRef}
          />
        </React.Fragment>
      )
    }
  }

  openLoginPanel() {
    this.setState({
      login: true
    });
  }

  closeLoginPanel() {
    this.setState({
      login: false
    });
  }

  render() {
    return (
      <div className="app">
        <TopBar
          onChangeRightPanel={this.changeRightPanel.bind(this)}
          changeResults={this.changeProductResults}
          goToLastState={this.goToLastState}
          openLogin={this.openLoginPanel.bind(this)}
        />
        <div className="panels">
          {this.renderLeftPanel()}
        </div>
        <Nav
          resetCatalog={this.resetCatalog.bind(this)}
          changeLastPanel={this.changeLastPanel.bind(this)}
          onChangeRightPanel={this.changeRightPanel.bind(this)}
        />
      </div>
    );
  }
}

export default App;
