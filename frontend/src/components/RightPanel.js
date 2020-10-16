
import '../assets/css/RightPanel.css';
import React from 'react';
import Catalog from './Catalog';
import Cart from './Cart';
import Map from './Map';

class RightPanel extends React.Component {
  constructor(props) {
    super(props);
  }

  currentPanel(props) {
    let panel = props.panel;
    if(panel === "cart") {
      return <Cart/>
    } else if(panel === "catalog"){
      return <Catalog/>
    } else if(panel === "map"){
      return <Map/>
    }
  }

  render() {
    return (
        <div class="right-panel">
          <this.currentPanel panel={this.props.panel}/>
        </div>
    );
  }
}

export default RightPanel;