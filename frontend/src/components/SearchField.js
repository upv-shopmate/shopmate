import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';
import clearButton from '../assets/images/clear_button.png';
import Input from './Input';
import '../assets/css/Nav.css';
import { requestSearchDataBase } from "../requests/SearchRequests.js"

const UNSELECTED_BUTTON_COLOR = 'grey';


class SearchField extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      value: '',
    });
    this.changePanel = this.changePanel.bind(this);
    this.search = this.search.bind(this);
    this.updateSearchText = this.updateSearchText.bind(this);
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

  search() {
    var products = this.requestDataBase(this.state.searchInput);
    console.log(products);
  }

  async requestDataBase() {
    var products = await requestSearchDataBase(this.state.searchInput);
    return products
  }

  updateSearchText(input) {
    this.setState({
      searchInput: input
    });
  }

  render() {
    return (
      <div className="search-field">
        <img onClick={this.search} src={searchIcon} className="search-icon shadow"></img>
        <Input onChangeParent={this.updateSearchText} placeholder={"Buscar productos"} />
        <img className="clear-button" src={clearButton}></img>
      </div>
    );
  }
}

export default SearchField;
