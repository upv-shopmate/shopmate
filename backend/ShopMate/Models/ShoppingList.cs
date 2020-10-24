using ShopMate.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public decimal SubtotalPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

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
        }

        /// <summary>
        /// Quita de la lista la cantidad de productos indicada en el parametro de entrada entry.
        /// Si entry.Quantity >= ShoppingListEntry.Quantity de la lista Entries se eliminará la entrada de la lista.
        /// Devuelve true si existía el elemento en la lista antes de invocar al método, en caso contrario devuelve false.
        /// Se actualiza la variable Price.
        /// </summary>
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