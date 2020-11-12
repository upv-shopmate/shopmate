/* eslint react/prop-types: 0 */
import '../assets/css/ProductDetails.css';
import React, {Component} from 'react';
import '../assets/css/Languages.css';
import spainFlag from '../assets/images/Languages/spain_flag.png';
import uk_usFlag from '../assets/images/Languages/uk-eeuu_flag.png';
import valenciaFlag from '../assets/images/Languages/valencia_flag.png';


class Languages extends Component {
  constructor(props) {
    super(props);
    this.closeLanguagePanel = this.closeLanguagePanel(this);
  }

  closeLanguagePanel() {
    //this.props.closePanel();
  }

  render() {
    return (
    <div className="languagesPanel">
        <div className="languages">
            <button className="spanish" onClick={() => {
              
            }} id= "es"> 
                <img src={spainFlag} className="flag-icon"></img>
                <div className="language-name">Español</div>
            </button>   
            <button className="english" onClick={() => {
              
            }} id="en"> 
                <img src={uk_usFlag} className="flag-icon"></img>
                <div className="language-name">English</div>
            </button>    
            <button className="valencian" onClick={() => {
              
            }} id="val"> 
                <img src={valenciaFlag} className="flag-icon"></img>
                <div className="language-name">Valencià</div>
            </button>
        </div>
        <div className="languageButtonLine">
            <button className="changeLanguageButton" 
              onClick={() => {
                //cambio de idioma
              this.closeLanguagePanel();
              }}> Aceptar
        </button>
        </div>
    </div>
    
  );
  }
}

export default Languages;
