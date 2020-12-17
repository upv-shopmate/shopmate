/* eslint react/prop-types: 0 */

import '../assets/css/Coupon.css';
import React from 'react';
import {withTranslation} from 'react-i18next';

class Coupon extends React.Component {
    constructor(props) {
        super(props);
        this.controlCoupon = this.controlCoupon.bind(this);
    }

    controlCoupon(){
        if (this.isCouponApplied()){
            this.props.removeCoupon(this.props.coupon);
        }
        else{
            this.props.applyCoupon(this.props.coupon);
        }
    }

    isCouponApplied(){
        return this.props.appliedCoupons.includes(this.props.coupon);
    }

    renderAppliedIndicatorText(){
        const {t} = this.props;
        let text;
        if (this.isCouponApplied()){
            text = <div className="coupon-applied-indicator">{t('applied')}</div>;
        }
        else{
            text = <div className="coupon-not-applied-indicator">{t('not-applied')}</div>;
        }
        return text;
    }

    render() {
        return (
          <div className="coupon" 
          onClick={this.controlCoupon}>
            <div className="coupon-texts">
                <div className="coupon-name">{this.props.coupon.name}</div>
                <div className="coupon-code">{this.props.coupon.code}</div>
                {this.renderAppliedIndicatorText()}
            </div>
          </div>
        );
    }
}

export default withTranslation()(Coupon);