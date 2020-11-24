/* eslint react/prop-types: 0 */

import connectionGif from '../assets/images/no_connection.gif';
import React, {Component} from 'react';
import '../assets/css/ErrorPanel.css';
class ErrorPanel extends Component {
  constructor(props) {
    super(props);
  }

  closePanel() {
    this.props.closeErrorPanel();
  }

  render() {
    return (
      <div className="ep-window">
        <div className="ep-panel">
          <div className="ep-title">
                        ¡Córcholis!
          </div>
          <div className="ep-subtitle">
                        Parece que ha habido un problema
                        con la conexión del carrito.
                        No te preocupes, se solucionará enseguida.
          </div>
          <img className="ep-loading-gif" src={connectionGif}></img>
        </div>
      </div>
    );
  }
}

export default ErrorPanel;
