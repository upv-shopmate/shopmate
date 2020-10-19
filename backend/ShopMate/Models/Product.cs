#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ShopMate.Models
{
    public class Product : IEquatable<Product>
    {
        /// <summary>
        /// The unique barcode of this product in GTIN-14 format.
        /// </summary>
        [Key]
        public Gtin14 Barcode { get; internal set; }

        [MaxLength(120)]
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
        /// The ISO 3166-1 Alpha-2 country code (e.g. "ES") of the country where this product was manufactured or grown.
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
        public ICollection<Brand> Brands { get; internal set; } = new HashSet<Brand>();

        /// <summary>
        /// A set of descriptive tags used to enhance the product search and recommendation features.
        /// </summary>
        public ICollection<Category> Categories { get; internal set; } = new HashSet<Category>();

        /// <summary>
        /// A set of awards, seals, certifications and regulatory labels that apply to this product (e.g. "Gluten Free").
        /// </summary>
        public ICollection<Label> Labels { get; internal set; } = new HashSet<Label>();

        /// <summary>
        /// The fixed modifiers that apply to the price of this product.
        /// </summary>
        public ICollection<PriceModifier> PriceModifiers { get; internal set; } = new List<PriceModifier>();

        /// <summary>
        /// The positions where this item is present.
        /// </summary>
        public ICollection<Position> Positions { get; internal set; } = new HashSet<Position>();

        /// <summary>
        /// The stores that sell this product.
        /// </summary>
        public ICollection<Store> Vendors { get; internal set; } = new HashSet<Store>();

        /// <summary>
        /// Obtains the price of this product with all the price modifiers applied.
        /// </summary>
        public decimal ModifiedPrice => PriceModifiers.Aggregate(Price, (price, modifier) => modifier.Apply(price));

        /// <summary>
        /// Obtains the price of this product with all the VAT modifiers applied.
        /// </summary>
        /// <remarks>
        /// This will display taxes included in product prices if is it required by law in the country it's used in.
        /// </remarks>
        public decimal PriceWithVat => PriceModifiers.Where(m => m.Code == PriceModifierCode.Vat).Aggregate(Price, (price, modifier) => modifier.Apply(price));

        public Product(Gtin14 barcode, string name, double? weight, double? volume, ushort? units, string? originCountry, bool edible, decimal price, ICollection<string> pictures, uint? availableStock, uint timesSold)
        {
            Barcode = barcode;
            Name = name;
            Weight = weight;
            Volume = volume;
            Units = units;
            OriginCountry = originCountry;
            Edible = edible;
            Price = price;
            Pictures = pictures;
            AvailableStock = availableStock;
            TimesSold = timesSold;
        }

        public override bool Equals(object? other) => other is Product && Equals(other);

        public bool Equals(Product? other) => Barcode == other?.Barcode;

        public static bool operator ==(Product lhs, Product rhs) => lhs.Equals(rhs);
        public static bool operator !=(Product lhs, Product rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Barcode.GetHashCode();
    }
}
