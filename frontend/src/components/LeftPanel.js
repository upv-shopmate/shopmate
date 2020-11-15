import '../assets/css/LeftPanel.css';
import listImage from '../assets/images/list.png';
import tagImage from '../assets/images/tag_icon.png';
import React from 'react';
import NotLoginPanel from './NotLoginPanel';

class LeftPanel extends React.Component {
  constructor(props) {
    super(props);
  }

  openLoginPanel() {
    this.props.openLogin()
  }

  render() {
    return (
      <div className="left-panel">
        <div className="left-panel-title">Lista de la compra</div>
        <div className="current-panel">
          <NotLoginPanel openLogin={this.openLoginPanel.bind(this)} />
        </div>
        <div className="bottom-buttons">
          <button
            className="button"
          >
            <img className="list-button-image" src={listImage}></img>
            <div className="list-button-text">MIS LISTAS</div>
          </button>
          <button
            className="button"
          >
            <img className="tag-button-image" src={tagImage}></img>
            <div className="tag-button-text">CUPONES</div>
          </button>
        </div>
      </div>
    );
  }
}

export default LeftPanel;
