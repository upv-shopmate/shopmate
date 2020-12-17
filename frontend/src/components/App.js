import '../assets/css/App.css';
import React from 'react';
import TopBar from './TopBar';
import LeftPanel from './LeftPanel';
import RightPanel from './RightPanel';
import Nav from './Nav';
import {requestSearchDataBase} from '../requests/SearchRequests.js';
import Login from './Login';
import {userInfoRequest, userListsRequest, userCouponsList} from '../requests/UserRequests.js';
import {requestAllCategories} from '../requests/CategoriesRequests';
import {requestProductById} from '../requests/ProductRequest';
import UserDetails from './UserDetails';
import ZoomedImage from './ZoomedImage';
import ErrorPanel from './ErrorPanel';
import {getStore} from '../utils/Store';

export const dataBaseURL = 'https://localhost:5001';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      'selectedPanel': 'cart',
      'results': [],
      'lastPanel': '',
      'resultsPage': 0,
      'lastSearchInput': '',
      'noMoreResults': false,
      'login': false,
      'user': undefined,
      'accessToken': undefined,
      'panels': 'default',
      'connectionError': false,
      'buttonEnabled': false,
      'currentList': null,
      'cartContent': undefined,
      'zoomedImage': undefined,
      'lists': undefined,
      'coupons': undefined,
      'appliedCoupons': [],
    };
    this.addProductToCartContent = this.addProductToCartContent.bind(this);
    this.removeProductToCartContent =
      this.removeProductToCartContent.bind(this);
    this.resetCartContent = this.resetCartContent.bind(this);
    this.changeProductResults = this.changeProductResults.bind(this);
    this.goToLastState = this.goToLastState.bind(this);
    this.rightPanelRef = React.createRef();
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
  }

  async getUserInfo(accessToken) {
    this.setState({
      accessToken: accessToken,
    });

    let response;
    try {
      response = await userInfoRequest(accessToken);
      this.hideErrorPanel();
      if (response.status == 200) {
        this.setState({user: response.data});
        this.logInUser(response.data);
        this.getUserLists();
        this.getUserCoupons();
      }
    } catch (e) {
      this.showErrorPanel();
      this.getUserInfo(accessToken);
    }
  }

  async componentDidMount() {
    const store = getStore();
    store.subscribe(() => this.forceUpdate());
    await this.initializeCart();
    await this.initializeCategories();
  }

  async initializeCategories() {
    let data;
    const store = getStore();
    try {
      data = await requestAllCategories();
      this.hideErrorPanel();
      store.dispatch({
        type: 'changeCategories',
        categories: data,
      });
    } catch (e) {
      this.showErrorPanel();
      this.initializeCategories();
    }
  }

  async fillCart() {
    const firstProduct = await requestProductById(13204);
    const secondProduct = await requestProductById(10502);
    const thirthProduct = await requestProductById(14116);
    this.addProductToCartContent(firstProduct);
    this.addProductToCartContent(firstProduct);
    this.addProductToCartContent(firstProduct);
    this.addProductToCartContent(secondProduct);
    this.addProductToCartContent(secondProduct);
    this.addProductToCartContent(thirthProduct);
  }

  async initializeCart() {
    try {
      this.setState({
        cartContent: {
          'entries': [],
          'subtotalPrice': 0,
          'totalPrice': 0,
          'modifierBreakdowns': [],
        },
      });
      this.hideErrorPanel();
      await this.fillCart();
    } catch (e) {
      this.showErrorPanel();
      this.initializeCart();
    }
  }

  addProductToCartContent(product) {
    const updatedContent = this.state.cartContent;
    let productAlreadyInside = false;
    updatedContent.entries.forEach((element) => {
      if (element.item.id === product.id) {
        productAlreadyInside = true;
        element.quantity = element.quantity + 1;
        element.totalPrice =
          element.quantity * element.item.modifiedPrice;
      }
    });
    if (!productAlreadyInside) {
      const newItem = {
        'item': product,
        'quantity': 1,
        'totalPrice': product.modifiedPrice,
        'priceModifiers': product.priceModifiers,
      };
      updatedContent.entries.push(newItem);
    }
    updatedContent.subtotalPrice = updatedContent.subtotalPrice + product.price;
    updatedContent.totalPrice =
      updatedContent.totalPrice + product.modifiedPrice;
    this.addModifiersToCartContent(product, updatedContent, 1);
    this.setState({
      cartContent: updatedContent,
    });
  }

  addModifiersToCartContent(product, updatedContent, quantity) {
    const modifiers = [...product.priceModifiers];
    updatedContent.modifierBreakdowns.forEach((storedModifier) => {
      modifiers.forEach((productModifier) => {
        if (storedModifier.modifier.code === productModifier.code &&
          storedModifier.modifier.value === productModifier.value) {
          storedModifier.applicableBase = storedModifier.applicableBase + product.price * quantity;
          storedModifier.totalDelta =
            storedModifier.applicableBase * productModifier.value * quantity;
          modifiers.splice(modifiers.indexOf(productModifier), 1);
        }
      });
    });
    modifiers.forEach((modifier) => {
      const newModifier = {
        'applicableBase': product.price * quantity,
        'modifier': modifier,
        'totalDelta': product.price * quantity * modifier.value,
      };
      updatedContent.modifierBreakdowns.push(newModifier);
    });
  }

  removeProductToCartContent(product) {
    const updatedContent = this.state.cartContent;
    let productInside = false;
    updatedContent.entries.forEach((element) => {
      if (element.item.id === product.id) {
        productInside = true;
        if (element.quantity === 1) {
          const index = updatedContent.entries.indexOf(element);
          updatedContent.entries.splice(index, 1);
        } else {
          element.quantity = element.quantity - 1;
          element.totalPrice = element.quantity * element.item.modifiedPrice;
        }
      }
    });
    if (productInside) {
      updatedContent.subtotalPrice = updatedContent.subtotalPrice - product.price;
      updatedContent.totalPrice = updatedContent.totalPrice - product.modifiedPrice;
      this.removeModifiersFromCartContent(product, updatedContent, 1);
      this.setState({
        cartContent: updatedContent,
      });
    }
    return productInside;
  }

  removeModifiersFromCartContent(product, updatedContent, quantity) {
    const modifiers = [...product.priceModifiers];
    updatedContent.modifierBreakdowns.forEach((storedModifier) => {
      modifiers.forEach((productModifier) => {
        if (storedModifier.modifier.code === productModifier.code &&
          storedModifier.modifier.value === productModifier.value) {
          storedModifier.applicableBase = storedModifier.applicableBase - product.price * quantity;
          storedModifier.totalDelta =
            storedModifier.applicableBase * productModifier.value * quantity;
          modifiers.splice(modifiers.indexOf(productModifier), 1);
          if (storedModifier.applicableBase === 0) {
            const indexModifier =
              updatedContent.modifierBreakdowns.indexOf(storedModifier);
            updatedContent.modifierBreakdowns.splice(indexModifier, 1);
          }
        }
      });
    });
  }

  resetCartContent() {
    const resettedContent = this.state.cartContent;
    resettedContent.entries = [];
    resettedContent.subtotalPrice = 0;
    resettedContent.totalPrice = 0;
    resettedContent.modifierBreakdowns = [];
    this.setState({
      cartContent: resettedContent,
    });
  }

  async getUserLists() {
    let response;
    try {
      response = await userListsRequest(this.state.accessToken);
      this.hideErrorPanel();
      if (response.status == 200) this.setState({lists: response.data});
    } catch (e) {
      this.showErrorPanel();
      this.getUserLists(this.state.accessToken);
    }
  }

  async getUserCoupons() {
    let response;
    try {
      response = await userCouponsList(this.state.accessToken);
      this.hideErrorPanel();
      if (response.status == 200) this.setState({coupons: response.data});
    } catch (e) {
      this.showErrorPanel();
      this.getUserCoupons(this.state.accessToken);
    }
  }

  applyCoupon(coupon) {
    const cartContent = this.state.cartContent;
    cartContent.entries.forEach((entry) => {
      if (coupon.applicableProducts.length === 0 ||
        coupon.applicableProducts.includes(entry.item)) {
        this.removeModifiersFromCartContent(entry.item, cartContent, entry.quantity);
        cartContent.totalPrice = cartContent.totalPrice - entry.item.modifiedPrice * entry.quantity;
        entry.item.priceModifiers = entry.item.priceModifiers.concat(coupon.effects);
        const modifierSum = this.getModifierSum(entry.item.priceModifiers);
        entry.item.modifiedPrice = entry.item.price * modifierSum;
        entry.totalPrice = entry.quantity * entry.item.modifiedPrice;
        cartContent.totalPrice = cartContent.totalPrice + entry.item.modifiedPrice * entry.quantity;
        this.addModifiersToCartContent(entry.item, cartContent, entry.quantity);
      }
    });
    const appliedCoupons = this.state.appliedCoupons.concat(coupon);
    this.setState({
      cartContent: cartContent,
      appliedCoupons: appliedCoupons,
    });
  }

  removeCoupon(coupon) {
    const cartContent = this.state.cartContent;
    cartContent.entries.forEach((entry) => {
      if (coupon.applicableProducts.length === 0 ||
        coupon.applicableProducts.includes(entry.item)) {
        this.removeModifiersFromCartContent(entry.item, cartContent, entry.quantity);
        cartContent.totalPrice = cartContent.totalPrice - entry.item.modifiedPrice * entry.quantity;
        entry.item.priceModifiers.forEach((modifier) => {
          if (coupon.effects.includes(modifier)) {
            const index = entry.item.priceModifiers.indexOf(modifier);
            entry.item.priceModifiers.splice(index, 1);
          }
        });
        const modifierSum = this.getModifierSum(entry.item.priceModifiers);
        entry.item.modifiedPrice = entry.item.price * modifierSum;
        entry.totalPrice = entry.quantity * entry.item.modifiedPrice;
        cartContent.totalPrice = cartContent.totalPrice + entry.item.modifiedPrice * entry.quantity;
        this.addModifiersToCartContent(entry.item, cartContent, entry.quantity);
      }
    });
    const appliedCoupons = this.state.appliedCoupons;
    appliedCoupons.splice(appliedCoupons.indexOf(coupon), 1);
    this.setState({
      cartContent: cartContent,
      appliedCoupons: appliedCoupons,
    });
  }

  removeAllCoupons() {
    this.state.appliedCoupons.forEach((coupon) => {
      this.removeCoupon(coupon);
    });
  }

  getModifierSum(modifiers) {
    let finalMultiplier = 1;
    modifiers.forEach((modifier) => {
      finalMultiplier = finalMultiplier + modifier.value;
    });
    return finalMultiplier;
  }

  logInUser(user) {
    this.setState({
      'login': true,
      'user': user,
    });
    this.enableListsButton();
  }

  logOutUser() {
    this.removeAllCoupons();
    this.setState({
      'user': undefined,
      'accessToken': undefined,
      'login': false,
    });
    this.setDefaultPanel();
    this.disableListsButton();
  }

  getAndChangeResultsPage() {
    const page = this.state.resultsPage;
    this.setState({
      'resultsPage': page + 1,
    });
    return page + 1;
  }

  getCurrentList(list) {
    if (this.state.currentList == null ||
      this.state.currentList.id != list.id) {
      this.setState({
        'currentList': list,
      });
    }
  }

  resetCatalog() {
    if (this.rightPanelRef !== undefined) {
      this.rightPanelRef.current.resetCatalog();
    }
  }

  resetResults() {
    this.setState({
      'resultsPage': 0,
      'results': [],
    });
  }

  changeLastPanel(panel) {
    this.setState({
      lastPanel: panel,
    });
  }

  goToLastState() {
    getStore().changePanel(this.state.lastPanel);
    this.resetResults();
  }

  changeRightPanel(panel) {
    this.setState({
      'selectedPanel': panel,
    });
  }

  userLogOut() {
    this.setState({
      'user': undefined,
      'accessToken': undefined,
    });
  }

  enableListsButton() {
    let button = document.querySelector('.lf-list-button');
    if (button != null) {
      button.style.opacity = 1;
      button = document.querySelector('.lf-tag-button');
      button.style.opacity = 1;
      this.setState({
        'buttonEnabled': true,
      });
    }
  }

  disableListsButton() {
    let button = document.querySelector('.lf-list-button');
    if (button != null) {
      button.style.opacity = 0.4;
      button = document.querySelector('.lf-tag-button');
      button.style.opacity = 0.4;
      this.setState({
        'buttonEnabled': false,
      });
    }
  }

  changeProductResults(searchInput) {
    let page = 0;
    const lastSearchInput = this.state.lastSearchInput;
    let noMoreResults = this.state.noMoreResults;

    if (searchInput === undefined || searchInput === lastSearchInput) {
      page = this.getAndChangeResultsPage();
      searchInput = lastSearchInput;
    } else if (lastSearchInput !== searchInput) {
      this.resetResults();
      this.setState({
        'lastSearchInput': searchInput,
        'noMoreResults': false,
      });
      noMoreResults = false;
    }
    if (!noMoreResults) {
      this.requestAndUpdateResults(searchInput, lastSearchInput, page);
    }
  }

  async requestAndUpdateResults(searchInput, lastSearchInput, page) {
    const rightPanelRef = this.rightPanelRef.current;

    rightPanelRef.showLoading();
    if (searchInput !== lastSearchInput) {
      rightPanelRef.changeCompletedSearchResultsPanel(false);
      rightPanelRef.scrollToTopResultsPanel();
      rightPanelRef.updateSearchPanel([]);
    }
    let result;
    try {
      result = await requestSearchDataBase(searchInput, page);
      this.hideErrorPanel();
      rightPanelRef.changeCompletedSearchResultsPanel(true);
      if (result.nextPage == null) {
        this.setState({
          'noMoreResults': true,
        });
      }

      const results = this.state.results.concat(result.items);
      this.setState({
        'results': results,
      });
      const store = getStore();
      store.dispatch({
        type: 'changeResults',
        results: results,
      });
      rightPanelRef.updateSearchPanel(this.state.results);

      rightPanelRef.hideLoading();
    } catch (e) {
      rightPanelRef.hideLoading();
      this.showErrorPanel();
      this.requestAndUpdateResults(searchInput, lastSearchInput, page);
    }
  }

  getRightPanel() {
    const panel = getStore().getState().panel;
    return panel;
  }

  renderPanels() {
    if (this.state.panels === 'login') {
      return (
        <React.Fragment>
          {}
          <div className="panels">
            <Login
              loginUser={this.getUserInfo.bind(this)}
              closeLogin={this.setDefaultPanel.bind(this)}
            />
          </div>
        </React.Fragment>
      );
    } else if (this.state.panels === 'default') {
      return (
        <React.Fragment>
          {this.renderZoomedImage()}
          <div className="panels">
            <LeftPanel
              openLogin={this.openLoginPanel.bind(this)}
              user={this.state.user}
              userLoggedIn={this.state.login}
              buttonEnabled={this.state.buttonEnabled}
              enableListsButton={this.enableListsButton.bind(this)}
              disableListsButton={this.disableListsButton.bind(this)}
              lists={this.state.lists}
              coupons={this.state.coupons}
              appliedCoupons={this.state.appliedCoupons}
              applyCoupon={this.applyCoupon.bind(this)}
              removeCoupon={this.removeCoupon.bind(this)}
            />
            <RightPanel
              panel={this.getRightPanel()}
              goToLastState={this.goToLastState}
              results={this.state.results}
              moreResults={this.changeProductResults}
              cartContent={this.state.cartContent}
              ref={this.rightPanelRef}
              zoomImage={this.showZoomedImage.bind(this)}
              addProductToCartContent={this.addProductToCartContent}
              removeProductToCartContent={this.removeProductToCartContent}
              resetCartContent={this.resetCartContent}
              appliedCoupons={this.state.appliedCoupons}
            />
          </div>
          <Nav
            resetCatalog={this.resetCatalog.bind(this)}
            changeLastPanel={this.changeLastPanel.bind(this)}
            onChangeRightPanel={this.changeRightPanel.bind(this)}
          />
        </React.Fragment>
      );
    } else if (this.state.panels === 'userdetails') {
      return (
        <React.Fragment>
          <div className="panels">
            <UserDetails
              user={this.state.user}
              logOut={this.logOutUser.bind(this)}
              closeUserDetails={this.setDefaultPanel.bind(this)}
            />
          </div>
        </React.Fragment>
      );
    }
  }

  showZoomedImage(url) {
    this.setState({
      'zoomedImage': url,
    });
  }

  hideZoomedImage() {
    this.setState({
      'zoomedImage': undefined,
    });
  }

  renderZoomedImage() {
    const image = this.state.zoomedImage;
    if (image != undefined) {
      return (
        <React.Fragment>
          <ZoomedImage
            image={image}
            unZoomImage={this.hideZoomedImage.bind(this)} />
        </React.Fragment >
      );
    } else {
      return null;
    }
  }

  setDefaultPanel() {
    this.setState({
      'panels': 'default',
    });
  }

  openUserDetailsPanel() {
    this.setState({
      'panels': 'userdetails',
    });
  }

  openLoginPanel() {
    this.setState({
      'panels': 'login',
    });
  }

  showErrorPanel() {
    this.setState({
      'connectionError': true,
    });
  }

  hideErrorPanel() {
    this.setState({
      'connectionError': false,
    });
  }

  renderErrorPanel() {
    const store = getStore();
    const connectionError = store.getState().error;
    if (connectionError) {
      return (
        <React.Fragment>
          <ErrorPanel closeErrorPanel={this.hideErrorPanel.bind(this)} />
        </React.Fragment>
      );
    }
  }

  render() {
    return (
      <div className="app">
        <TopBar
          logOut={this.logOutUser.bind(this)}
          onChangeRightPanel={this.changeRightPanel.bind(this)}
          changeResults={this.changeProductResults}
          goToLastState={this.goToLastState}
          openUserDetails={this.openUserDetailsPanel.bind(this)}
          openLogin={this.openLoginPanel.bind(this)}
          user={this.state.user}
          userLoggedIn={this.state.login}
        />
        {this.renderErrorPanel()}
        {this.renderPanels()}
      </div>
    );
  }
}

export default App;
