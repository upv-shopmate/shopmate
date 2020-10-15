import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';

class App extends React.Component {
  render() {
    return (
      <div class="app">
        <TopBar/>
        <div class="panels">
          <LeftPanel/>
          <RightPanel/>
        </div>
        <Nav/>
      </div>
    );
  }
}

export default App;
