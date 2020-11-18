/* eslint react/prop-types: 0 */
import '../assets/css/TopBar.css';
import React from 'react';
import logo from '../assets/images/logo.png';
import exitIcon from '../assets/images/leave_icon.png';
import translateIcon from '../assets/images/translate.png';
import SearchField from './SearchField';
import Languages from './Languages';
import { useTranslation } from 'react-i18next';
import { bind } from 'file-loader';


 
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
    this.closeLanguagePanel = this.closeLanguagePanel.bind(this);
    this.langRef = React.createRef();
    
  }

  togglePopup() {
    this.setState({
      showPopup: !this.state.showPopup
    });
  }
  
  renderLanguages(){
    if(this.state.showPopup === true)
      return <Languages 
      closeLanguagePanel = {this.closeLanguagePanel()} >
         ref={this.langRef}
         </Languages>;
    else return null;
  }
  
  handleClick(lang){
    /*const {t, i18n} = useTranslation();*/
    i18n.changeLanguage(lang);
   }

   closeLanguagePanel() {
     this.setState = {
      showPopup: false
     }
   }
  
  render() {
    //const {t, i18n} = useTranslation();
    return (
       <div className="top-bar"> 
        <div className="left-side">
          <img src={logo}></img>
          <div className="logo-name"> 
            </div>
          <div className= 'prove-buttons'>
            <button className='es'
              onClick = {() => handleClick('es')}
              /> Español
            <button className='en'
            onClick = {() => handleClick('en')}
              /> English
            <button className='val'
            onClick = {() => handleClick('val')}
              /> Valenciano
          </div>
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
//<p>{t('ShopMate.1')}</p>