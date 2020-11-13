#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class User : IEquatable<User>
    {
        public int Id { get; private set; }

        /// <summary>
        /// The name of this client.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// The email of this client.
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        /// The phone of this client.
        /// </summary>
        public string Phone { get; internal set; }

        /// <summary>
        /// Money that this client has spent.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal MoneySpent { get; internal set; }

        public string Password { get; internal set; }   // FIXME use salted hashes like a decent human being

        public ICollection<Coupon> OwnedCoupons { get; set; } = new HashSet<Coupon>();

        public User(string name, string email)
        {
            Name = name;
            Email = email;
            MoneySpent = 0;
        }

        public override bool Equals(object? other) => other is User User && Equals(User);

        public bool Equals(User? other) => Id == other?.Id;

        public static bool operator ==(User lhs, User rhs) => lhs.Equals(rhs);
        public static bool operator !=(User lhs, User rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id.GetHashCode();
    }
}
