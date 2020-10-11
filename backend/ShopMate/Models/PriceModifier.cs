#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{

    public class PriceModifier
    {
        public int Id { get; private set; }

        public PriceModifierCode Code { get; internal set; }

        public string? Description { get; internal set; }

        [Column(TypeName = "money")]
        public decimal Value { get; internal set; }

        public PriceModifierKind Kind { get; internal set; }

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
