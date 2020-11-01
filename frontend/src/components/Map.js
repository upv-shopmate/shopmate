import '../assets/css/Map.css';
import React from 'react';

// 853x566
// 85x56
class Map extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'map': props.map,
    }
  }
  render() {
    return (
      <div className="map">
        {this.state.map}
      </div>
    );
  }
}

export default Map;
