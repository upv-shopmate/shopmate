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
    
  }

  closeDetailsPanel() {
    this.props.closePanel();
  }


  render() {
    return (
    <div className="languagesPanel">
        <div className="languages">
            <div className="spanish">
                <img src={spainFlag} className="flag-icon"></img>
                <div className="langauge-name">Español</div>
            </div>   
            <div className="english">
                <img src={uk_usFlag} className="flag-icon"></img>
                <div className="langauge-name">English</div>
            </div>    
            <div className="valencian">
                <img src={valenciaFlag} className="flag-icon"></img>
                <div className="langauge-name">Valencià</div>
            </div>
        </div>
        <div className="languageButtonLine">
            <button className="changeLanguageButton" onClick={() => {
          
            }}>
                Aceptar
        </button>
        </div>
    </div>
    
  );
  }
}

export default Languages;
