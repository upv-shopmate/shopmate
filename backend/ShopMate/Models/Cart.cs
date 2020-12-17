#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShopMate.Models
{
    public class Cart : IEquatable<Cart>
    {
        public int Id { get; private set; }

        public bool Active { get; internal set; }

        public Store Owner { get; internal set; }

        public Cart() { }

        public override bool Equals(object? other) => other is Cart cart && Equals(cart);

        public bool Equals(Cart? other) => Id == other?.Id;

        public static bool operator ==(Cart lhs, Cart rhs) => lhs.Equals(rhs);
        public static bool operator !=(Cart lhs, Cart rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
