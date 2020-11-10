import '../assets/css/LeftPanel.css';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';

class LeftPanel extends React.Component {
  render() {
    return (
      <div className="left-panel">
        <div className="left-panel-title">Lista de la compra</div>
        <div className="current-panel">
          <NotLoginPanel/>
        </div>
        <div className="bottom-buttons">
          <button className="button">MIS LISTAS</button>
          <button className ="button">CUPONES</button>   
        </div>
      </div>
    );
  }
}

export default LeftPanel;
