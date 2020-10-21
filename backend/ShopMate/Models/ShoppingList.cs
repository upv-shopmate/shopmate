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

        public IReadOnlyCollection<ShoppingListEntry> Entries { get => entries; }

        readonly HashSet<ShoppingListEntry> entries = new HashSet<ShoppingListEntry>();

        public ShoppingList(string name)
        {
            Name = name;
        }

        public void ChangeName(string newName) => Name = newName;

        public void AddProduct(ShoppingListEntry entry)
        {
            if (entries.TryGetValue(entry, out ShoppingListEntry? currentEntry))
            {
                currentEntry.Quantity += entry.Quantity;
            } 
            else
            {
                entries.Add(entry);
            }
        }

        public void DeleteProduct(ShoppingListEntry entry)
        {
            if (entries.TryGetValue(entry, out ShoppingListEntry? currentEntry))
            {
                if (currentEntry.Quantity <= entry.Quantity)
                {
                    entries.Remove(currentEntry);
                }
                else
                {
                    currentEntry.Quantity -= entry.Quantity;
                }
            }
        }

        public void DeleteProduct(Product product)
        {

        }

        public override bool Equals(object? other) => other is ShoppingList list && Equals(list);

        public bool Equals(ShoppingList? other) => Id == other?.Id;

        public static bool operator ==(ShoppingList lhs, ShoppingList rhs) => lhs.Equals(rhs);
        public static bool operator !=(ShoppingList lhs, ShoppingList rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Name.GetHashCode();
    }
}