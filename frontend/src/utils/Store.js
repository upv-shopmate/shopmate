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
        state.listeners.forEach(l => l());
    };

    const showError = (show) => {
        dispatch({
            type: "showError",
            error: show
        })
    }

    return {
        subscribe,
        getState,
        dispatch,
        showError
    };
}

function reducer(state, action) {
    if (action.type === 'changeCurrentList') {
        state["currentList"] = action.currentList;
    } else if (action.type === 'changeResults') {
        state["results"] = action.results;
    } else if (action.type === 'showError') {
        state["error"] = action.error;
    };
    return state;
}

const initialState = {
    results: [], 
    currentList: null, 
    listeners: [],
    error: false, 
};
export function Store() {
    var instance;

    function createInstance() {
        const store = createStore(reducer, initialState);
        return store;
    }
    return {
        getInstance: () => {
            if(!instance) {
                instance = createInstance();
            }
            return instance;
        }
    }

};
