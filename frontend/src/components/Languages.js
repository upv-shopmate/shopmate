/* eslint react/prop-types: 0 */
import '../assets/css/ProductDetails.css';
import React, {Component} from 'react';
import '../assets/css/Languages.css';
import spainFlag from '../assets/images/Languages/spain_flag.png';
import ukFlag from '../assets/images/Languages/uk-eeuu_flag.png';
import valenciaFlag from '../assets/images/Languages/valencia_flag.png';
import {withTranslation} from 'react-i18next';


class Languages extends Component {
  constructor(props) {
    super(props);
    this.focusRef = React.createRef();
    this.focusLost = this.focusLost.bind(this);
  }

  componentDidMount(){
    document.addEventListener('click', this.focusLost);
  }

  componentWillUnmount(){
    document.removeEventListener('click', this.focusLost);
  }

  handleClick(lang) {
    this.props.i18n.changeLanguage(lang);
    this.props.togglePopup();
  }

  focusLost(event){
    if(this.focusRef && !this.focusRef.current.contains(event.target)){
      this.props.togglePopup();
    }
  }

  render() {
    const {t} = this.props;
    return (
      <div className="languagesPanel"
          ref = {this.focusRef}>
        <div className="languages">
          <button className="spanish" onClick={() => {
            this.handleClick('es');
          }} id= "es">
            <img src={spainFlag} className="flag-icon"></img>
            <div className="language-name"> {t('spanish')}</div>
          </button>
          <button className="english" onClick={() => {
            this.handleClick('en');
          }} id="en">
            <img src={ukFlag} className="flag-icon"></img>
            <div className="language-name">{t('english')}</div>
          </button>
          <button className="valencian" onClick={() => {
            this.handleClick('ca');
          }} id="val">
            <img src={valenciaFlag} className="flag-icon"></img>
            <div className="language-name">{t('valencian')}</div>
          </button>
        </div>
      </div>
    );
  }
}

export default withTranslation()(Languages);
