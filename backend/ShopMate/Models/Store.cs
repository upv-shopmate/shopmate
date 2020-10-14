#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class Store : IEquatable<Store>
    {
        public int Id { get; private set; }

        public string Name { get; internal set; }

        /// <summary>
        /// The ISO-4217 currency code (e.g "EUR") of the currency used by this store.
        /// </summary>
        [Column(TypeName = "char(3)")]
        public string Currency { get; internal set; }

        public ICollection<Product> Products { get; internal set; } = new HashSet<Product>();

        public Store(string name, string currency)
        {
            Name = name;
            Currency = currency;
        }

        public override bool Equals(object? other) => other is Store && Equals(other);

        public bool Equals(Store? other) => Name == other?.Name;

        public static bool operator ==(Store lhs, Store rhs) => lhs.Equals(rhs);
        public static bool operator !=(Store lhs, Store rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Name.GetHashCode();
    }
}
