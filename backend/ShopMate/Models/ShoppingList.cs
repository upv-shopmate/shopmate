using ShopMate.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShopMate.Models
{
    public class ShoppingList : IEquatable<ShoppingList>, IBuyableList<Product>
    {
        public int Id { get; private set; }

        [MaxLength(50)]
        public string Name { get; internal set; }

        public IReadOnlyCollection<IBuyableListEntry<Product>> Entries { get => entries; }
        readonly HashSet<IBuyableListEntry<Product>> entries = new HashSet<IBuyableListEntry<Product>>();

        [Column(TypeName = "money")]
        public decimal SubtotalPrice { get; private set; }
        [Column(TypeName = "money")]
        public decimal TotalPrice { get; private set; }

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns { get => breakdowns; }
        readonly HashSet<PriceModifierBreakdown> breakdowns = new HashSet<PriceModifierBreakdown>();

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
            if (entries.TryGetValue(entry, out IBuyableListEntry<Product>? currentEntry))
            {
                currentEntry.Quantity += entry.Quantity;
            } 
            else
            {
                entries.Add(entry);
            }

            SubtotalPrice += entry.Quantity * entry.Item.Price;
            TotalPrice += entry.Quantity * entry.Item.ModifiedPrice;

            foreach (var newBreakdown in entry.ModifierBreakdowns)
            {
                if (breakdowns.TryGetValue(newBreakdown, out PriceModifierBreakdown? currentBreakdown))
                {
                    currentBreakdown += newBreakdown;
                }
                else
                {
                    breakdowns.Add(newBreakdown);
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
            if (entries.TryGetValue(entry, out IBuyableListEntry<Product>? currentEntry))
            {
                if (currentEntry.Quantity <= entry.Quantity)
                {
                    entries.Remove(currentEntry);
                }
                else
                {
                    currentEntry.Quantity -= entry.Quantity;
                }

                SubtotalPrice -= entry.Quantity * entry.Item.Price;
                TotalPrice -= entry.Quantity * entry.Item.Price;

                foreach (var newBreakdown in entry.ModifierBreakdowns)
                {
                    if (breakdowns.TryGetValue(newBreakdown, out PriceModifierBreakdown? currentBreakdown))
                    {
                        currentBreakdown -= newBreakdown;

                        if (newBreakdown.ApplicableBase <= 0)
                        {
                            breakdowns.Remove(newBreakdown);
                        }
                    }
                }

                return true;
            }
            return false;
        }

        public bool RemoveEntry(Product item)
        {
            var entry = entries.Where(e => e.Item == item).FirstOrDefault();

            if (entry is null)
            {
                return false;
            }

            entries.Remove(entry);
            return false;
        }

        public override bool Equals(object? other) => other is ShoppingList list && Equals(list);

        public bool Equals(ShoppingList? other) => Id == other?.Id;

        public static bool operator ==(ShoppingList lhs, ShoppingList rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingList lhs, ShoppingList rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}