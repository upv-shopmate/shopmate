using ShopMate.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class ShoppingList : IEquatable<ShoppingList>, IBuyableList<Product>
    {
        public int Id { get; private set; }

        [MaxLength(50)]
        public string Name { get; internal set; }

        public IReadOnlyCollection<IBuyableListEntry<Product>> Entries { get => (IReadOnlyCollection<IBuyableListEntry<Product>>)entries.Values; }
        readonly IDictionary<Product, IBuyableListEntry<Product>> entries = new Dictionary<Product, IBuyableListEntry<Product>>();

        [Column(TypeName = "money")]
        public decimal SubtotalPrice { get; internal set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; internal set; }

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns { get => (IReadOnlyCollection<PriceModifierBreakdown>)breakdowns.Values; }
        readonly IDictionary<PriceModifier, PriceModifierBreakdown> breakdowns = new Dictionary<PriceModifier, PriceModifierBreakdown>();

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
        public void AddEntry(IBuyableListEntry<Product> entry)
        {
            if (entries.TryGetValue(entry.Item, out IBuyableListEntry<Product>? currentEntry))
            {
                currentEntry.Quantity += entry.Quantity;
            } 
            else
            {
                entries.Add(entry.Item, entry);
            }

            SubtotalPrice += entry.Quantity * entry.Item.Price;
            TotalPrice += entry.Quantity * entry.Item.ModifiedPrice;

            foreach (var newBreakdown in entry.ModifierBreakdowns)
            {
                if (breakdowns.TryGetValue(newBreakdown.Modifier, out PriceModifierBreakdown? currentBreakdown))
                {
                    breakdowns[newBreakdown.Modifier] = currentBreakdown + newBreakdown;
                }
                else
                {
                    breakdowns.Add(newBreakdown.Modifier, newBreakdown);
                }
            }
        }

        /// <summary>
        /// Remove the given quantity of product of the list of entries.
        /// </summary>
        /// <remarks>
        /// The corresponding entry already present will be updated or removed to reflect the new quantity after the subtraction.
        /// </remarks>
        public bool RemoveEntry(IBuyableListEntry<Product> entry)
        {
            if (entries.TryGetValue(entry.Item, out IBuyableListEntry<Product>? currentEntry))
            {
                if (currentEntry.Quantity <= entry.Quantity)
                {
                    entries.Remove(entry.Item);
                }
                else
                {
                    currentEntry.Quantity -= entry.Quantity;
                }

                SubtotalPrice -= entry.Quantity * entry.Item.Price;
                TotalPrice -= entry.Quantity * entry.Item.ModifiedPrice;

                foreach (var newBreakdown in entry.ModifierBreakdowns)
                {
                    if (breakdowns.TryGetValue(newBreakdown.Modifier, out PriceModifierBreakdown? currentBreakdown))
                    {
                        breakdowns[newBreakdown.Modifier] = currentBreakdown - newBreakdown;

                        if (newBreakdown.ApplicableBase <= 0)
                        {
                            breakdowns.Remove(newBreakdown.Modifier);
                        }
                    }
                }

                return true;
            }
            return false;
        }

        public bool RemoveEntry(Product item) => entries.Remove(item);

        public override bool Equals(object? other) => other is ShoppingList list && Equals(list);

        public bool Equals(ShoppingList? other) => Id == other?.Id;

        public static bool operator ==(ShoppingList lhs, ShoppingList rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingList lhs, ShoppingList rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}