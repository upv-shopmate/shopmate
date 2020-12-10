using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShopMate.Models
{
    public class ShoppingList : IEquatable<ShoppingList>
    {
        public int Id { get; private set; }

        [MaxLength(50)]
        public string Name { get; internal set; }

        public ICollection<ShoppingListEntry> Entries { get; private set; } = new List<ShoppingListEntry>();

        [Column(TypeName = "money")]
        public decimal SubtotalPrice => Entries.Select(e => e.Price).Sum();
        [Column(TypeName = "money")]
        public decimal TotalPrice => Entries.Select(e => e.ModifiedPrice).Sum();

        public ICollection<Coupon> AppliedCoupons { get; set; } = new HashSet<Coupon>();

        public User? Owner { get; internal set; }

        [NotMapped]
        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns
        {
            get
            {
                var dictionary = new Dictionary<PriceModifier, PriceModifierBreakdown>();
                foreach (var entry in Entries)
                {
                    foreach (var breakdown in entry.ModifierBreakdowns)
                    {
                        if (dictionary.TryGetValue(breakdown.Modifier, out _))
                        {
                            dictionary[breakdown.Modifier] += breakdown;
                        }
                        else
                        {
                            dictionary.Add(breakdown.Modifier, breakdown);
                        }
                    }
                }

                return dictionary.Values;
            }
        }

        public ShoppingList(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Change the name of this shopping list.
        /// </summary>
        public void ChangeName(string newName) => Name = newName;

        /// <summary>
        /// Add or update a product with its quantity to the list of entries.
        /// </summary>
        /// <remarks>
        /// If a corresponding entry is already present it will be updated to reflect the new quantity the addition.
        /// </remarks>
        public void AddEntry(ShoppingListEntry entry)
        {
            var oldEntry = Entries.Where(e => e.Item == entry.Item);
            if (oldEntry.Any())
            {
                oldEntry.First().Quantity += entry.Quantity;
            }
            else
            {
                Entries.Add(entry);
            }
        }

        /// <summary>
        /// Remove the given quantity of product of the list of entries.
        /// </summary>
        /// <remarks>
        /// The corresponding entry already present will be updated or removed to reflect the new quantity after the subtraction.
        /// </remarks>
        public bool RemoveEntry(ShoppingListEntry entry)
        {
            var oldEntry = Entries.Where(e => e.Item == entry.Item);
            if (oldEntry.Any())
            {
                var newQuantity = Math.Max(oldEntry.First().Quantity - entry.Quantity, 0);

                if (oldEntry.First().Quantity <= 0)
                {
                    Entries.Remove(oldEntry.First());
                }
                else
                {
                    oldEntry.First().Quantity = newQuantity;
                }

                return true;
            }

            return false;
        }

        public void ApplyCoupon(Coupon discount)
        {
            if (!AppliedCoupons.Contains(discount))
            {
                AppliedCoupons.Add(discount);
                ActualizeEntryModifiers();
            }
        }

        public void UnapplyCoupon(Coupon discount)
        {
            if (AppliedCoupons.Contains(discount))
            {
                AppliedCoupons.Remove(discount);
                ActualizeEntryModifiers();
            }
        }

        private void ActualizeEntryModifiers()
        {
            foreach (var entry in Entries)
            {
                entry.AdditionalModifiers.Clear();
                entry.AdditionalModifiers.AddRange(AppliedCoupons
                    .Where(c => !c.ApplicableProducts.Any() || c.ApplicableProducts.Contains(entry.Item))
                    .SelectMany(c => c.Effects));
            }
        }

        public override bool Equals(object? other) => other is ShoppingList list && Equals(list);

        public bool Equals(ShoppingList? other) => Id == other?.Id;

        public static bool operator ==(ShoppingList lhs, ShoppingList rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingList lhs, ShoppingList rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}