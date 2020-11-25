import '../assets/css/UserList.css';
import React from 'react';

class UserList extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return(
            <div className="planned-list">
                <div className="top-elements">
                    <div className="name-list">Fiesta de Paco</div>
                </div>
                <div className="bot-elements">
                    <div className="list-price">Precio planificado: 80 €</div>
                </div>
            </div>
        );
    }
}

export default UserList;