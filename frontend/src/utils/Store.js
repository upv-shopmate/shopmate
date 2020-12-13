function createStore(reducer, initialState) {
  let state = initialState;

  const subscribe = (listener) => {
    state.listeners.push(listener);
  };

  const getState = () => (state);

  const dispatch = (action) => {
    state = reducer(state, action);
    // call each listener function when the state is changed
    // its just a notification that state is changed
    state.listeners.forEach((l) => l());
  };

  const showError = (show) => {
    dispatch({
      type: 'showError',
      error: show,
    });
  };

  const changePanel = (panel) => {
    dispatch({
      type: 'changePanel',
      panel: panel,
    });
  };

  const showProduct = (productInMap) => {
    dispatch({
      type: 'showProduct',
      productInMap: productInMap,
    });
  };

  return {
    subscribe,
    getState,
    dispatch,
    showError,
    changePanel,
    showProduct,
  };
}

function reducer(state, action) {
  if (action.type === 'changeCurrentList') {
    state['currentList'] = action.currentList;
  } else if (action.type === 'changePanel') {
    state['panel'] = action.panel;
  } else if (action.type === 'changeResults') {
    state['results'] = action.results;
  } else if (action.type === 'showProduct') {
    state['productInMap'] = action.productInMap;
  } else if (action.type === 'showError') {
    state['error'] = action.error;
  } else if (action.type === 'changeCategories') {
    state["categories"] = action.categories;
  };
  return state;
}

const initialState = {
  results: [],
  currentList: null,
  listeners: [],
  error: false,
  panel: 'cart',
  productInMap: null,
};
let instance;
function createInstance() {
  const store = createStore(reducer, initialState);
  return store;
}
export function getStore() {
  if (!instance) {
    instance = createInstance();
  }
  return instance;
}
