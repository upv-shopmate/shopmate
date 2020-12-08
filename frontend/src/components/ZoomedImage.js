import React, { Component } from 'react';
import '../assets/css/ZoomedImage.css';
import closeButton from '../assets/images/close_24px.png';
import pageLeftImage from '../assets/images/page-left.png';
import pageRightImage from '../assets/images/page-right.png';

class ZoomedImage extends Component {
    constructor(props) {
        super(props);
        this.hidePanel = this.hidePanel.bind(this);
    }

    changeImageSize() {
        let image = this.props.image;
        image = image.substring(0, image.length - 21) + "?fit=crop&h=600&w=600"; //change crop size
        return image;
    }

    hidePanel() {
        this.props.unZoomImage();
    }

    render() {
        return (
            <div className="zi-panel">
                <img className="zi-closebutton"
                    src={closeButton}
                    onClick={this.hidePanel}
                ></img>
                <img src={this.changeImageSize()} className="zi-image"></img>
            </div>
        );
    }
}

export default ZoomedImage;