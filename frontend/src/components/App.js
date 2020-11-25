import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';
import { requestSearchDataBase } from '../requests/SearchRequests.js';
import Login from './Login';
import {userInfoRequest, userListsRequest} from '../requests/UserRequests.js';
import {requestCatalog} from '../requests/ProductRequest.js';
import UserDetails from './UserDetails';
import ErrorPanel from './ErrorPanel';

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
      'panels': 'default',
      'connectionError': false,
      'buttonEnabled': false,
      'currentList': null,
    };
    this.changeProductResults = this.changeProductResults.bind(this);
    this.goToLastState = this.goToLastState.bind(this);
    this.rightPanelRef = React.createRef();
  }

  async getUserInfo(accessToken) {
    this.setState({
      accessToken: accessToken,
    });

    let response;
    try {
      response = await userInfoRequest(accessToken);
      this.hideErrorPanel();
      if (response.status == 200) {
        this.setState({ user: response.data });
          this.logInUser(response.data);
          this.getUserLists();
      }
    } catch (e) {
      this.showErrorPanel();
      this.getUserInfo(accessToken);
    }
  }

  async getUserLists(){
    let response;
    try {
      response = await userListsRequest(this.state.accessToken);
      this.hideErrorPanel();
      if (response.status == 200) this.setState({lists : response.data});
    } catch (e) {
      this.showErrorPanel();
      this.getUserLists(this.state.accessToken);
    }
  }

  logInUser(user) {
    this.setState({
      'login': true,
      'user': user
    });
    this.enableListsButton();
  }

  logOutUser() {
    this.setState({
      'user': undefined,
      'accessToken': undefined,
      'login': false,
    });
    this.setDefaultPanel();
    this.disableListsButton();
  }

  getAndChangeResultsPage() {
    const page = this.state.resultsPage;
    this.setState({
      'resultsPage': page + 1,
    });
    return page + 1;
  }

  getCurrentList(list){
    if(this.state.currentList == null || this.state.currentList.id != list.id){
      this.setState({
        'currentList' : list
      });  
      console.log(list);
    }
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
      'accessToken': undefined,
    });
  }

  enableListsButton() {
    const button = document.querySelector('.lf-list-button');
    button.style.opacity = 1;
    this.setState({
      'buttonEnabled': true,
    });
  }

  disableListsButton() {
    const button = document.querySelector('.lf-list-button');
    if (button != null) {
      button.style.opacity = 0.4;
      this.setState({
        'buttonEnabled': false,
      });
    }
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
    let result;
    try {
      result = await requestSearchDataBase(searchInput, page);
      this.hideErrorPanel();
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
    } catch (e) {
      rightPanelRef.hideLoading();
      this.showErrorPanel();
      this.requestAndUpdateResults(searchInput, lastSearchInput, page);
    }
  }

  renderPanels() {
    if (this.state.panels === 'login') {
      return (
        <React.Fragment>
          <div className="panels">
            <Login
              showErrorPanel={this.showErrorPanel.bind(this)}
              hideErrorPanel={this.hideErrorPanel.bind(this)}
              loginUser={this.getUserInfo.bind(this)}
              closeLogin={this.setDefaultPanel.bind(this)}
            />
          </div>
        </React.Fragment>
      );
    } else if (this.state.panels === 'default') {
      return (
        <React.Fragment>
          <div className="panels">
            <LeftPanel
              openLogin={this.openLoginPanel.bind(this)}
              user={this.state.user}
              userLoggedIn={this.state.login}
              buttonEnabled={this.state.buttonEnabled}
              lists={this.state.lists}
              onGetCurrentList={this.getCurrentList.bind(this)}
            />
            <RightPanel
              showErrorPanel={this.showErrorPanel.bind(this)}
              hideErrorPanel={this.hideErrorPanel.bind(this)}
              panel={this.state.selectedPanel}
              goToLastState={this.goToLastState}
              results={this.state.results}
              moreResults={this.changeProductResults}
              ref={this.rightPanelRef}
              currentList={this.state.currentList}
            />
          </div>
          <Nav
            resetCatalog={this.resetCatalog.bind(this)}
            changeLastPanel={this.changeLastPanel.bind(this)}
            onChangeRightPanel={this.changeRightPanel.bind(this)}
          />
        </React.Fragment>
      );
    } else if (this.state.panels === 'userdetails') {
      return (
        <React.Fragment>
          <div className="panels">
            <UserDetails
              user={this.state.user}
              logOut={this.logOutUser.bind(this)}
              closeUserDetails={this.setDefaultPanel.bind(this)}
            />
          </div>
        </React.Fragment>
      );
    }
  }

  setDefaultPanel() {
    this.setState({
      'panels': 'default',
    });
  }

  openUserDetailsPanel() {
    this.setState({
      'panels': 'userdetails',
    });
  }

  openLoginPanel() {
    this.setState({
      'panels': 'login',
    });
  }

  showErrorPanel() {
    this.setState({
      'connectionError': true,
    });
  }

  hideErrorPanel() {
    this.setState({
      'connectionError': false,
    });
  }

  renderErrorPanel() {
    if (this.state.connectionError) {
      return (
        <React.Fragment>
          <ErrorPanel closeErrorPanel={this.hideErrorPanel.bind(this)} />
        </React.Fragment>
      );
    }
  }

  render() {
    return (
      <div className="app">
        <TopBar
          logOut={this.logOutUser.bind(this)}
          onChangeRightPanel={this.changeRightPanel.bind(this)}
          changeResults={this.changeProductResults}
          goToLastState={this.goToLastState}
          openUserDetails={this.openUserDetailsPanel.bind(this)}
          openLogin={this.openLoginPanel.bind(this)}
          user={this.state.user}
          userLoggedIn={this.state.login}
        />
        {this.renderErrorPanel()}
        {this.renderPanels()}
      </div>
    );
  }
}

export default App;
