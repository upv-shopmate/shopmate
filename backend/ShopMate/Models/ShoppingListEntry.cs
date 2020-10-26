#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

using Microsoft.EntityFrameworkCore;
using ShopMate.Models.Interfaces;
using ShopMate.Models.Transient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Models
{
    [Owned]
    public class ShoppingListEntry : IEquatable<ShoppingListEntry>, IBuyableListEntry<Product>
    {
        public int Quantity { get; internal set; }
        int IBuyableListEntry<Product>.Quantity { get => Quantity; set => Quantity = value; }

        public Product Item { get; internal set; }

        public decimal Price => Quantity * Item.Price;

        public decimal ModifiedPrice => Quantity * Item.ModifiedPrice;

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns
        {
            get => Item.PriceModifiers.Select(modifier =>
                    new PriceModifierBreakdown(
                        modifier, 
                        applicableBase: Quantity * Item.Price, 
                        totalDelta: Quantity * modifier.DeltaFor(Item.Price)))
                    .ToList();
        }

        public ShoppingListEntry()
        { }

        public ShoppingListEntry(int quantity, Product product)
        {
            Quantity = quantity;
            Item = product;
        }

        public override bool Equals(object? other) => other is ShoppingListEntry entry && Equals(entry);

        public bool Equals(ShoppingListEntry? other) => Item == other?.Item!;

        public static bool operator ==(ShoppingListEntry lhs, ShoppingListEntry rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingListEntry lhs, ShoppingListEntry rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Item.GetHashCode();
    }
}
