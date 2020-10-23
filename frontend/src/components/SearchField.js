import '../assets/css/SearchField.css';
import React from 'react';
import searchIcon from '../assets/images/search_icon.png';

class SearchField extends React.Component {
  render() {
    return (
      <div className="search-field shadow">
        <img src={searchIcon} className="search-icon"></img>
        <input
          placeholder="Buscar productos, marcas, categorías"
          className="search-input"
        >
        </input>
      </div>
    );
  }
}

export default SearchField;
