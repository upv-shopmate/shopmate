#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Models
{
    public class Cart : IEquatable<Cart>
    {
        public int Id { get; private set; }

        public bool Active { get; internal set; }

        public decimal PlannedPrice => TrackedLists.Sum(list => list.TotalPrice);

        public Store Owner { get; internal set; }

        public ShoppingList Contents { get; internal set; } = new ShoppingList("");

        public ICollection<ShoppingList> TrackedLists { get; internal set; } = new HashSet<ShoppingList>();

        public ICollection<Coupon> AppliedCoupons { get; internal set; } = new HashSet<Coupon>();

        public Cart() { }

        public void ApplyCoupon(Coupon discount)
        {
            if (!AppliedCoupons.Contains(discount))
            {
                if (!discount.ApplicableProducts.Any())
                {
                    Contents.TotalPrice -= discount.Effects.Select(d => d.DeltaFor(Contents.SubtotalPrice)).Sum();
                }
                else
                {
                    foreach (ShoppingListEntry entry in Contents.Entries)
                    {
                        if (discount.ApplicableProducts.Contains(entry.Item))
                        {
                            Contents.TotalPrice -= discount.Effects.Select(d => d.DeltaFor(entry.Price)).Sum();
                        }
                    }
                }
            }
        }

        public void UnapplyCoupon(Coupon discount)
        {
            if (AppliedCoupons.Contains(discount))
            {
                if (!discount.ApplicableProducts.Any())
                {
                    Contents.TotalPrice += discount.Effects.Select(d => d.DeltaFor(Contents.SubtotalPrice)).Sum();
                }
                else
                {
                    foreach (ShoppingListEntry entry in Contents.Entries)
                    {
                        if (discount.ApplicableProducts.Contains(entry.Item))
                        {
                            Contents.TotalPrice += discount.Effects.Select(d => d.DeltaFor(entry.Price)).Sum();
                        }
                    }
                }
            }
        }

        public void TrackShoppingList(ShoppingList list) => TrackedLists.Add(list);

        public void UntrackShoppingList(ShoppingList list) => TrackedLists.Remove(list);

        public override bool Equals(object? other) => other is Cart cart && Equals(cart);

        public bool Equals(Cart? other) => Id == other?.Id;

        public static bool operator ==(Cart lhs, Cart rhs) => lhs.Equals(rhs);
        public static bool operator !=(Cart lhs, Cart rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
