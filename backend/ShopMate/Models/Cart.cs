#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class Cart : IEquatable<Cart>
    {
        public int Id { get; private set; }

        public bool Active { get; internal set; }

        public decimal PlannedPrice { get => TrackedLists.Sum(list => list.TotalPrice); }

        public virtual Store Owner { get; internal set; }

        public virtual ShoppingList Contents { get; internal set; } = new ShoppingList("");

        public virtual ICollection<ShoppingList> TrackedLists { get; internal set; } = new HashSet<ShoppingList>();

        public Cart() { }

        public void TrackShoppingList(ShoppingList list) => TrackedLists.Add(list);

        public void UntrackShoppingList(ShoppingList list) => TrackedLists.Remove(list);

        public override bool Equals(object? other) => other is Cart cart && Equals(cart);

        public bool Equals(Cart? other) => Id == other?.Id;

        public static bool operator ==(Cart lhs, Cart rhs) => lhs.Equals(rhs);
        public static bool operator !=(Cart lhs, Cart rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
