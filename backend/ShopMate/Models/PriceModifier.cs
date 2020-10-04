#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    public abstract class PriceModifier
    {
        public PriceModifierCode Code { get; internal set; }

        public string? Description { get; internal set; }

        public abstract decimal Value { get; internal set; }

        public abstract PriceModifierKind Kind { get; }

        public abstract decimal Apply(decimal basePrice);
    }

    public class AdditivePriceModifier : PriceModifier
    {
        [Column(TypeName = "money")]
        public override decimal Value { get; internal set; }

        public override PriceModifierKind Kind { get => PriceModifierKind.Additive; }

        public override decimal Apply(decimal basePrice)
            => basePrice + Value;
    }

    public class MultiplicativePriceModifier : PriceModifier
    {
        [Column(TypeName = "decimal(18, 2)")]
        public override decimal Value { get; internal set; }

        public override PriceModifierKind Kind { get => PriceModifierKind.Multiplicative; }

        public override decimal Apply(decimal basePrice)
            => basePrice * (1M + Value);
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
