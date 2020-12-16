import React, {Component} from 'react';
import Category from './Category';
import '../assets/css/CategoriesDropDown.css';
import clearButton from '../assets/images/clear_button.png';
import {getStore} from '../utils/Store';

class CatergoriesDropDown extends Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedCategory': undefined,
      'showCategories': false,
      'categories': undefined,
    };
  }

  getCategory() {
    const category = this.state.selectedCategory;
    if (category != undefined && category.parent != null) {
      return category.name;
    } else {
      return 'Seleccione una subcategoría';
    }
  }

  updateCategory() {
    this.props.updateCategory(this.state.selectedCategory);
  }

  setCategory(category) {
    if (category != this.state.selectedCategory) {
      this.setState({
        'selectedCategory': category,
      }, () => {
        if (category != undefined && category.parent != null) {
          this.toggleShowCategories();
          this.updateCategory();
        }
      });
    } else {
      this.setState({
        'selectedCategory': undefined,
      });
    }
  }

  sortCategories(categories) {
    return categories.sort(this.compareCategories);
  }

  compareCategories(a, b) {
    if (a.name < b.name) {
      return -1;
    } else {
      return 1;
    }
  }

  resetCategory() {
    this.setState({
      'selectedCategory': undefined,
    }, () => {
      this.updateCategory();
    });
  }


  renderClearButton() {
    if (this.state.selectedCategory != undefined) {
      return (
        <React.Fragment>
          <img
            className="cdd-clear-button"
            src={clearButton}
            onClick={() => {
              this.resetCategory();
            }}></img>
        </React.Fragment>
      );
    }
  }

  toggleShowCategories() {
    this.setState({
      'showCategories': !this.state.showCategories,
    });
  }

  getCategories() {
    const categories = getStore().getState().categories;
    return categories;
  }

  renderCategories() {
    let categories = this.getCategories();
    if (categories != undefined) {
      let aux = categories;
      aux = this.sortCategories(aux.filter((category) => category.parent == null));
      const selectedCategory = this.state.selectedCategory;
      if (selectedCategory != undefined && selectedCategory.parent == null) {
        categories = categories.filter((category) => category.parent != null);
        const children = this.sortCategories(
            categories.filter((category) => category.parent.id == selectedCategory.id),
        );
        const before = []; const after = []; let position;
        for (let i = 0; i < aux.length; i++) {
          before.push(aux[i]);
          if (aux[i] == this.state.selectedCategory) {
            position = i;
            i = aux.length;
          }
        }
        for (let i = position + 1; i < aux.length; i++) {
          after.push(aux[i]);
        }
        aux = before.concat(children);
        aux = aux.concat(after);
      } else {
        categories = aux;
      }
      return (aux.map((category) =>
        <Category
          key={category.id}
          category={category}
          setCategory={this.setCategory.bind(this)}
        />)
      );
    } else {
      return null;
    }
  }

  renderCategoriesPanel() {
    if (this.state.showCategories) {
      return (
        <div className="cdd-dropdown">
          {this.renderCategories()}
        </div>);
    }
  }


  render() {
    return (
      <div className="cdd-panel">
        <span className="cdd-text">Filtrar por categoría</span>
        <button className="cdd-button" onClick={() => {
          this.toggleShowCategories();
        }}>{this.getCategory()}</button>
        {this.renderClearButton()}
        {this.renderCategoriesPanel()}
      </div >
    );
  }
}

export default CatergoriesDropDown;
