#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;


namespace ShopMate.Models
{
    public class Coupon
    {
        public int Id { get; private set; }

        /// <summary>
        /// The name of the coupon.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The code asigned to the coupon.
        /// </summary>
        public string Code { get; internal set; }

        /// <summary>
        /// List of PriceModifiers for the coupon.
        /// </summary>
        public ICollection<PriceModifier> Effects { get; internal set; } = new HashSet<PriceModifier>();

        /// <summary>
        /// List of the aplicable products for this coupon.
        /// </summary>
        public ICollection<Product> ApplicableProducts { get; internal set; } = new HashSet<Product>();

        /// <summary>
        /// Store where the coupon is valid.
        /// </summary>
        public Store Store { get; internal set; }

        public Coupon(Store store) => Store = store;

        public override bool Equals(object? other) => other is Coupon Coupon && Equals(Coupon);

        public bool Equals(Coupon? other) => Id == other?.Id;

        public static bool operator ==(Coupon lhs, Coupon rhs) => lhs.Equals(rhs);
        public static bool operator !=(Coupon lhs, Coupon rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
