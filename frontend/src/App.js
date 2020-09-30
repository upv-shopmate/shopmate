import React, { useEffect } from 'react';
import './App.css';
const isElectron = require('is-electron');
if(isElectron()) {
    // here we can run electron modules
    let ipcRenderer = window.require('electron').ipcRenderer;
}


class App extends React.Component{
    componentDidMount() {

    }
    render() {
        return (
            <div className="App" onClick={() => {console.log(window);}}>
                efwfwefwwefwefw
            </div>
        );
    }
}

export default App;
