import "../assets/css/SearchResult.css";
import tagIcon from "../assets/images/tag_icon.png"
import imageNotFound from "../assets/images/image_not_found.jpg"
import React, { Component } from 'react';

class SearchResult extends Component {
    render() {
        return (
            <div className="result">
                <img className="result-image" src="https://images-na.ssl-images-amazon.com/images/I/81ZgygEckPL._AC_SX569_.jpg"></img>
                <div className="result-info">
                    <div className="result-topline">
                        <div className="result-name">Nesquik</div>
                        <div className="result-separator">·</div>
                        <div className="result-brand">Nestle</div>
                        <div className="result-separator">·</div>
                        <div className="result-weight">250g</div>
                    </div>
                    <div className="result-bottomline">
                        <div className="result-cart">En el carro: N/A</div>
                        <div className="result-categories">
                            <img className="result-categories-image" src={tagIcon}></img>
                            <div className="result-categories-namelist">Desayuno, azúcar, farlopa</div>
                        </div>
                    </div>
                </div>
                <div className="result-price">2,38€</div>
            </div>
        );
    }
}

export default SearchResult;