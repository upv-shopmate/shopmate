import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';
import Input from './Input';


class SearchField extends React.Component {
  render() {
    return (
      <div className="search-field">
        <img src={searchIcon} className="search-icon shadow"></img>
        <Input placeholder={"Buscar productos"} />
      </div>
    );
  }
}

export default SearchField;
