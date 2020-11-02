import "../assets/css/SearchPanel.css";
import leftArrow from "../assets/images/left_arrow.png"
import React from "react";
import Result from "./SearchResult"
import SearchResult from "./SearchResult";

class SearchPanel extends React.Component {
  render() {
    return (
      <div className="searcher">
        <div className="searcher-title">Resultados de la búsqueda (1)</div>
        <div className="searcher-results">
          <Result />
          <Result />
        </div>
        <div className="searcher-buttons">
          <button className="return-button">
            <img className="return-button-image" src={leftArrow}></img>
            <div className="return-button-text">VOLVER</div>
          </button>
        </div>
      </div>
    );
  }
}

export default SearchPanel;