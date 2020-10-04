#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public class Product : IEquatable<Product>
    {
        /// <summary>
        /// The unique barcode of this product in GTIN-14 format.
        /// </summary>
        [Key]
        public Gtin14 Barcode { get; internal set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; internal set; }

        /// <summary>
        /// The weight of a whole buyable entity of this product in grams.
        /// </summary>
        public double? Weight { get; internal set; }

        /// <summary>
        /// The volume of a whole buyable entity of this product in liters.
        /// </summary>
        public double? Volume { get; internal set; }

        /// <summary>
        /// The number of subunits a buyable entity of this product contains. Does not affect weight or volume calculations.
        /// </summary>
        public ushort? Units { get; internal set; }

        /// <summary>
        /// Country code (e.g. "ES") of the country where this product was manufactured or grown.
        /// </summary>
        [Column(TypeName = "char(2)")]
        public string? OriginCountry { get; internal set; }

        /// <summary>
        /// Whether if this product is food.
        /// </summary>
        public bool Edible { get; internal set; }

        /// <summary>
        /// The retail price of each buyable entity of this product in the current store's currency.
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Price { get; internal set; }

        /// <summary>
        /// The identifiers or paths to the pictures showing the appearance or features of this product.
        /// </summary>
        public ICollection<string> Pictures { get; internal set; } = new List<string>();

        /// <summary>
        /// The number of buyable entities left in the store for clients to buy.
        /// </summary>
        public uint? AvailableStock { get; internal set; }

        /// <summary>
        /// The accumulated number of times the available stock of this item has decreased one unit.
        /// </summary>
        public uint TimesSold { get; internal set; }

        /// <summary>
        /// The brands that are selling this product.
        /// </summary>
        public ICollection<Brand> Brands { get; internal set; } = new List<Brand>();

        /// <summary>
        /// A set of descriptive tags used to enhance the product search and recommendation features.
        /// </summary>
        public ICollection<Category> Categories { get; internal set; } = new List<Category>();

        public override bool Equals(object? other) => other is Product && this.Equals(other);

        public bool Equals(Product? other) => this.Barcode == other?.Barcode;

        public static bool operator ==(Product lhs, Product rhs) => lhs.Equals(rhs);
        public static bool operator !=(Product lhs, Product rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Barcode.GetHashCode();
    }
}
