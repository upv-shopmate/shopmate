/* eslint react/prop-types: 0 */

import '../assets/css/Coupon.css';
import React from 'react';
import {withTranslation} from 'react-i18next';

class Coupon extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
        };
    }

    render() {
        const {t} = this.props;
        return (
          <div className="coupon">
            <div className="coupon-logo"></div>
            <div className="coupon-texts">
                <div className="coupon-top">
                    <div className="coupon-name">¡Oferta día sin IVA!</div>
                </div>
                <div className="coupon-bot"></div>
            </div>
          </div>
        );
    }
}

export default withTranslation()(Coupon);