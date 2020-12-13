import React, { Component } from 'react';
import '../assets/css/Category.css'
class Category extends Component {
    constructor(props) {
        super(props);
        this.category = React.createRef();
    }

    componentDidMount() {
        this.checkIfSubCategory();
    }

    checkIfSubCategory() {
        const categoryCss = this.category.current;
        if (this.props.category.parent != null) {
            categoryCss.style.backgroundColor = "#eeeeee";
            categoryCss.style.marginTop = "1px";
            categoryCss.style.paddingLeft = "25px";
        }
    }

    setCategory() {
        this.props.setCategory(this.props.category);
    }

    getName() {
        if (this.props.category.parent == null) {
            return this.props.category.name;
        } else {
            return "- " + this.props.category.name;
        }
    }

    render() {
        return (
            <div ref={this.category} className="c-category" onClick={() => { this.setCategory() }}>
                {this.getName()}
            </div>);
    }
}

export default Category;