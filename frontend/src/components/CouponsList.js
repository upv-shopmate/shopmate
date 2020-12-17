/* eslint react/prop-types: 0 */
/* eslint no-irregular-whitespace: 2*/

import React from 'react';
import Coupon from './Coupon';

class CouponsList extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const coupons = this.props.coupons;
    if (coupons != undefined && coupons.length > 0) {
      return coupons.map((coupon) =>
        <Coupon
          key={coupon.code}
          coupon={coupon}
          appliedCoupons={this.props.appliedCoupons}
          applyCoupon={this.props.applyCoupon}
          removeCoupon={this.props.removeCoupon}
        />,
      );
    }
  }
}

export default CouponsList;
