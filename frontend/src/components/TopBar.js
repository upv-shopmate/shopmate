/* eslint react/prop-types: 0 */
import '../assets/css/TopBar.css';
import React from 'react';
import logo from '../assets/images/logo.png';
import exitIcon from '../assets/images/leave_icon.png';
import translateIcon from '../assets/images/translate.png';
import SearchField from './SearchField';
import Languages from './Languages';

// TODO: extract user field to a new component, DO NOT implement user here
class TopBar extends React.Component { 
  constructor(props) {
    super(props);
    this.state = {
        showPopup: false,
        langSelected: "es"
    }
    this.togglePopup = this.togglePopup.bind(this);
    this.renderLanguages = this.renderLanguages.bind(this);
    this.langRef = React.createRef();
    
  }

  togglePopup() {
    this.setState({
      showPopup: !this.state.showPopup
    });
  }
  
  renderLanguages(){
    if(this.state.showPopup === true)
      return <Languages> 
         ref={this.langRef}/</Languages>;
    else return null;
  }
  
  
  render() {
    return (
      <div className="top-bar">
        <div className="left-side">
          <img src={logo}></img>
          <div className="logo-name">ShopMate</div>
        </div>
        <SearchField
          goToLastState={this.props.goToLastState}
          onChangeRightPanel={this.props.onChangeRightPanel}
          changeResults={this.props.changeResults}
        />
        <div className="right-side">
          <div className="user-field shadow">
            <div className="first-letter">Y</div>
            <div className="name">Yoel</div>
          </div>
          <div className="translate-button top-button shadow">
            <img src={translateIcon} onClick={this.togglePopup}/>
            {this.renderLanguages()}
          </div>
          <div className="exit-button top-button shadow">
            <img src={exitIcon}></img>
          </div>
        </div> 
      </div>
    );
  }
}

export default TopBar;