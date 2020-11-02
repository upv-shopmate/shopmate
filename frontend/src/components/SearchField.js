import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';
import clearButton from '../assets/images/clear_button.png';
import Input from './Input';
import '../assets/css/Nav.css';

const UNSELECTED_BUTTON_COLOR = 'grey';


class SearchField extends React.Component {
  constructor(props) {
    super(props);
    this.changePanel = this.changePanel.bind(this);
  }

  changePanel() {
    this.props.onChangeRightPanel('searcher');
    this.unselectEveryButton();
  }

  unselectEveryButton() {
    const buttons = document.querySelectorAll('.nav-button');
    buttons.forEach((element) => {
      element.style.backgroundColor = UNSELECTED_BUTTON_COLOR;
    });
  }

  render() {
    return (
      <div className="search-field">
        <img onClick={this.changePanel} src={searchIcon} className="search-icon shadow"></img>
        <Input placeholder={"Buscar productos"} />
        <img className="clear-button" src={clearButton}></img>
      </div>
    );
  }
}

export default SearchField;
