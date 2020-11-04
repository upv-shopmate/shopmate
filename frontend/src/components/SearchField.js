/* eslint react/prop-types: 0 */
import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';
import clearButton from '../assets/images/clear_button.png';
import Input from './Input';
import '../assets/css/Nav.css';
import {requestSearchDataBase} from '../requests/SearchRequests.js';

class SearchField extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      value: '',
    });
    this.changePanel = this.changePanel.bind(this);
    this.search = this.search.bind(this);
    this.updateSearchText = this.updateSearchText.bind(this);
    this.clearSearchField = this.clearSearchField.bind(this);
    this.inputRef = React.createRef();
  }

  changePanel() {
    this.props.onChangeRightPanel('searcher');
  }

  async search() {
    if (this.state.searchInput.length > 0) {
      const products = await requestSearchDataBase(this.state.searchInput);
      this.props.changeResults(products);
      this.changePanel();
    } else {
      this.closeSearchPanel();
    }
  }

  closeSearchPanel() {
    this.props.goToLastState();
  }

  updateSearchText(input) {
    this.setState({
      searchInput: input,
    });
  }


  clearSearchField() {
    this.inputRef.current.clearFieldValue();
    this.closeSearchPanel();
  }

  render() {
    return (
      <div className="search-field">
        <img
          onClick={this.search}
          src={searchIcon}
          className="search-icon shadow">
        </img>
        <Input
          ref={this.inputRef}
          onChangeParent={this.updateSearchText}
          placeholder={'Buscar productos, marcas, categorías...'}
          search={this.search}
        />
        <img
          className="clear-button"
          onClick={this.clearSearchField}
          src={clearButton}>
        </img>
      </div>
    );
  }
}

export default SearchField;
