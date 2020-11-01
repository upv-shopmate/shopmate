import '../assets/css/Square.css';
import React from 'react';

let COLORS = {
    0: "white",
    1: "black",
    2: "#717171",
    3: "#83F8FF",
    4: "#FF6B6B",
    5: "#A7FF99",
    6: "#FFA451",
};
class Square extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            'color': props.color,
            x: props.x,
            y: props.y,
            shadow: props.shadow,
        }
        this.square = React.createRef();
    }
    componentDidMount() {
        this.square.current.style.backgroundColor = COLORS[this.state.color];
        if (this.state.color !== 0 && this.state.shadow === true) {
            this.square.current.classList.add("shadow");
        }
    }
    render() {
        return (
            <div className="square" ref={this.square} id={this.state.x + " " + this.state.y}>
            </div>
        );
    }
}

export default Square;