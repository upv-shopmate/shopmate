import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedPanel': 'cart'
    };
  }
  changeRightPanel(panel) {
    this.setState({
      'selectedPanel': panel,
    });
  }
  render() {
    return (
      <div className="app">
        <TopBar onChangeRightPanel={this.changeRightPanel.bind(this)} />
        <div className="panels">
          <LeftPanel />
          <RightPanel panel={this.state.selectedPanel} />
        </div>
        <Nav onChangeRightPanel={this.changeRightPanel.bind(this)} />
      </div>
    );
  }
}

export default App;
