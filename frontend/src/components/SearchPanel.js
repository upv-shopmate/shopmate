/* eslint react/prop-types: 0 */
import '../assets/css/SearchPanel.css';
import leftArrow from '../assets/images/left_arrow.png';
import React from 'react';
import Result from './SearchResult';

class SearchPanel extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'results': [],
    }
  }


  handleScroll(element) {
    const bottomPosition = element.target.scrollHeight - element.target.offsetHeight;
    const currentPosition = element.target.scrollTop;
    if (currentPosition === bottomPosition) {
      this.props.onResultsBottomPage();
    }
  }

  renderResults() {
    return this.state.results.map((result) =>
      <Result key={result.barcode} productData={result} />,
    );
  }

  updateResults(input) {
    this.setState({
      'results': input
    })
  }

  getResultsNumber() {
    const results = this.state.results.length;
    if (results > 0) {
      return 'Resultados de la búsqueda (' + results + ')';
    }
    return 'No se han encontrado resultados. Inténtelo de nuevo.';
  }

  render() {
    return (
      <div className="searcher">
        <div className="searcher-title">{this.getResultsNumber()}</div>
        <div className="searcher-results" onScroll={(e) => this.handleScroll(e)}>
          {this.renderResults()}
        </div>
        <div className="searcher-buttons">
          <button className="return-button" onClick={this.props.goToLastState}>
            <img className="return-button-image" src={leftArrow}></img>
            <div className="return-button-text">VOLVER</div>
          </button>
        </div>
      </div>
    );
  }
}

export default SearchPanel;
