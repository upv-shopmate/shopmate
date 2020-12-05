/* eslint react/prop-types: 0 */
/* eslint no-irregular-whitespace: 2*/

import '../assets/css/CouponsList.css';
import React from 'react';
import {withTranslation} from 'react-i18next';

class CouponsList extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const {t} = this.props;
    return (
      <div className="coupons-list">
        <Coupon></Coupon>
      </div>
    );
  }
}

export default withTranslation()(CouponsList);