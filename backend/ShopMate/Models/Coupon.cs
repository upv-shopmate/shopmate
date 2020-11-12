#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopMate.Models
{
    public class Coupon
    {
        /// <summary>
        /// The name of this coupon.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The code asigned to this coupon.
        /// </summary>
        [Key]
        public string Code { get; internal set; }

        /// <summary>
        /// List of PriceModifiers for this coupon.
        /// </summary>
        public ICollection<PriceModifier> Effects { get; internal set; } = new HashSet<PriceModifier>();

        /// <summary>
        /// List of the applicable products for this coupon.
        /// </summary>
        public ICollection<Product> ApplicableProducts { get; internal set; } = new HashSet<Product>();

        /// <summary>
        /// Store where this coupon is valid.
        /// </summary>
        public Store Store { get; internal set; }

        public Coupon()
        { }

        public override bool Equals(object? other) => other is Coupon Coupon && Equals(Coupon);

        public bool Equals(Coupon? other) => Code == other?.Code;

        public static bool operator ==(Coupon lhs, Coupon rhs) => lhs.Equals(rhs);
        public static bool operator !=(Coupon lhs, Coupon rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Code.GetHashCode();
    }
}
