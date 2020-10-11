#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{

    public class PriceModifier : IEquatable<PriceModifier>
    {
        public int Id { get; private set; }

        public PriceModifierCode Code { get; internal set; }

        public string? Description { get; internal set; }

        [Column(TypeName = "money")]
        public decimal Value { get; internal set; }

        public PriceModifierKind Kind { get; internal set; }

        public PriceModifier(PriceModifierCode code, string? description, decimal value, PriceModifierKind kind)
        {
            Code = code;
            Description = description;
            Value = value;
            Kind = kind;
        }

        public decimal Apply(decimal basePrice)
        {
            if (Kind == PriceModifierKind.Additive)
            {
                return basePrice + Value;
            }
            else
            {
                return basePrice * (1M + Value);
            }
        }

        public override bool Equals(object? other) => other is PriceModifier && this.Equals(other);

        public bool Equals(PriceModifier? other) => this.Id == other?.Id;

        public static bool operator ==(PriceModifier lhs, PriceModifier rhs) => lhs.Equals(rhs);
        public static bool operator !=(PriceModifier lhs, PriceModifier rhs) => !lhs.Equals(rhs);

        public override int GetHashCode() => Id;
    }

    public enum PriceModifierCode
    {
        Vat,
        Discount,
        Other
    }

    public enum PriceModifierKind
    {
        Multiplicative,
        Additive
    }
}
