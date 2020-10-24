using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopMate.Models
{
    public class ShoppingList : IEquatable<ShoppingList>
    {
        public int Id { get; private set; }

        [MaxLength(50)]
        public string Name { get; internal set; }

        public decimal Price { get; internal set; }

        public IReadOnlyCollection<ShoppingListEntry> Entries { get => entries; }

        readonly HashSet<ShoppingListEntry> entries = new HashSet<ShoppingListEntry>();

        /// <summary>
        /// Constructor de la lista de la compra.
        /// </summary>
        public ShoppingList(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Cambia el nombre de la lista de la compra.
        /// </summary>
        public void ChangeName(string newName) => Name = newName;

        /// <summary>
        /// Añade un producto con su cantidad a la lista de Entries.
        /// Si ese producto ya estaba en la lista se le suma la cantidad indicada en el parametro de entrada entry.
        /// Se actualiza la variable Price.
        /// </summary>
        public void AddProduct(ShoppingListEntry entry)
        {
            if (entries.TryGetValue(entry, out ShoppingListEntry? currentEntry))
            {
                currentEntry.Quantity += entry.Quantity;
                Price += entry.Quantity * entry.Product.Price;
            } 
            else
            {
                entries.Add(entry);
                Price += entry.Quantity * entry.Product.Price;
            }
        }

        /// <summary>
        /// Quita de la lista la cantidad de productos indicada en el parametro de entrada entry.
        /// Si entry.Quantity >= ShoppingListEntry.Quantity de la lista Entries se eliminará la entrada de la lista.
        /// Devuelve true si existía el elemento en la lista antes de invocar al método, en caso contrario devuelve false.
        /// Se actualiza la variable Price.
        /// </summary>
        public bool DeleteProduct(ShoppingListEntry entry)
        {
            if (entries.TryGetValue(entry, out ShoppingListEntry? currentEntry))
            {
                if (currentEntry.Quantity <= entry.Quantity)
                {
                    entries.Remove(currentEntry);
                    Price -= currentEntry.Quantity * entry.Product.Price;
                }
                else
                {
                    currentEntry.Quantity -= entry.Quantity;
                    Price -= entry.Quantity * entry.Product.Price;
                }
                return true;
            }
            return false;
        }

        public override bool Equals(object? other) => other is ShoppingList list && Equals(list);

        public bool Equals(ShoppingList? other) => Id == other?.Id;

        public static bool operator ==(ShoppingList lhs, ShoppingList rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingList lhs, ShoppingList rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}