function createStore(reducer, initialState) {
    let state = initialState;

    // Setup listners to keep track of when the state is changed
    // to triger rerenders (observer pattern)
    const listeners = [];

    const subscribe = (listener) => (
        listeners.push(listener)
    );

    const getState = () => (state);

    const dispatch = (action) => {
        state = reducer(state, action);
        console.log(state)
        // call each listener function when the state is changed
        // its just a notification that state is changed
        listeners.forEach(l => l());
    };

    return {
        subscribe,
        getState,
        dispatch,
    };
}

function reducer(state, action) {
    if (action.type === 'changeCurrentList') {
        return {
            panel: action.currentList,
        }
    };
    return state;
}

const initialState = {currentList: [] };
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
