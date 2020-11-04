#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopMate.Models
{
    public class Brand : IEquatable<Brand>
    {
        public int Id { get; private set; }

        /// <summary>
        /// The canonical name of this brand.
        /// </summary>
        [MaxLength(50)]
        public string Name { get; internal set; }

        /// <summary>
        /// Other names this brand is also known as.
        /// </summary>
        public ICollection<string> Aliases { get; internal set; }

        /// <summary>
        /// A identifier or path to the canonical logotype of this brand.
        /// </summary>
        public string? Logo { get; internal set; }

        /// <summary>
        /// The products this brand is selling.
        /// </summary>
        public ICollection<Product> Products { get; internal set; } = new HashSet<Product>();

        public Brand(string name, ICollection<string> aliases, string? logo)
        {
            Name = name;
            Aliases = aliases;
            Logo = logo;
        }

        public override bool Equals(object? other) => other is Brand brand && Equals(brand);

        public bool Equals(Brand? other) => Name == other?.Name;

        public static bool operator ==(Brand lhs, Brand rhs) => lhs.Equals(rhs);
        public static bool operator !=(Brand lhs, Brand rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Name.GetHashCode();
    }
}
