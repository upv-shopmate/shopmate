/* eslint react/prop-types: 0 */
import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';
import clearButton from '../assets/images/clear_button.png';
import Input from './Input';
import '../assets/css/Nav.css';
import {withTranslation} from 'react-i18next';
import {getStore} from '../utils/Store.js';

class SearchField extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      searchInput: '',
    });
    this.changePanel = this.changePanel.bind(this);
    this.search = this.search.bind(this);
    this.updateSearchText = this.updateSearchText.bind(this);
    this.clearSearchField = this.clearSearchField.bind(this);
    this.inputRef = React.createRef();
  }

  changePanel() {
    const store = getStore();
    store.changePanel('searcher');
  }

  search() {
    const searchInput = this.state.searchInput;
    if (searchInput.length > 0 && searchInput.trim().length > 0) {
      this.props.changeResults(searchInput);
      this.changePanel();
    } else {
      this.closeSearchPanel();
    }
  }

  closeSearchPanel() {
    this.resetSearchInput();
    this.props.goToLastState();
  }

  updateSearchText(input) {
    this.setState({
      searchInput: input,
    });
  }


  clearSearchField() {
    this.inputRef.current.clearFieldValue();
    this.resetSearchInput();
    this.closeSearchPanel();
  }

  resetSearchInput() {
    this.setState({
      searchInput: '',
    });
  }

  render() {
    const {t} = this.props;
    const placeholder = t('searchField.placeholder');
    return (
      <div className="search-field">
        <img
          onClick={this.search}
          src={searchIcon}
          className="search-icon">
        </img>
        <Input
          ref={this.inputRef}
          onChangeParent={this.updateSearchText}
          placeholder={placeholder}
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

export default withTranslation()(SearchField);
