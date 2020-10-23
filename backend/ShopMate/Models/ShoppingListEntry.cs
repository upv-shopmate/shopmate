#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMate.Models
{
    [Owned]
    public class ShoppingListEntry : IEquatable<ShoppingListEntry>
    {
        public int Quantity { get; internal set; }

        public Product Product { get; internal set; }

        public ShoppingListEntry()
        { }

        public ShoppingListEntry(int quantity, Product product)
        {
            Quantity = quantity;
            Product = product;
        }

        public override bool Equals(object? other) => other is ShoppingListEntry entry && Equals(entry);

        public bool Equals(ShoppingListEntry? other) => Product == other?.Product!;

        public static bool operator ==(ShoppingListEntry lhs, ShoppingListEntry rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingListEntry lhs, ShoppingListEntry rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Product.GetHashCode();
    }
}
