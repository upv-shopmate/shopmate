import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';

export const dataBaseURL = 'https://localhost:5001';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedPanel': 'cart',
      'results': [],
      'lastPanel': '',
    };
    this.changeProductResults = this.changeProductResults.bind(this);
    this.goToLastState = this.goToLastState.bind(this);
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
  }

  changeRightPanel(panel) {
    this.setState({
      'selectedPanel': panel,
    });
  }

  changeProductResults(input) {
    this.setState({
      results: input,
    });
  }

  render() {
    return (
      <div className="app">
        <TopBar onChangeRightPanel={this.changeRightPanel.bind(this)} changeResults={this.changeProductResults} />
        <div className="panels">
          <LeftPanel />
          <RightPanel panel={this.state.selectedPanel} goToLastState={this.goToLastState} results={this.state.results} />
        </div>
        <Nav changeLastPanel={this.changeLastPanel.bind(this)} onChangeRightPanel={this.changeRightPanel.bind(this)} />
      </div>
    );
  }
}

export default App;
