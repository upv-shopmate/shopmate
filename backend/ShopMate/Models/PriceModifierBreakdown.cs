#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace ShopMate.Models
{
    [NotMapped]
    public class PriceModifierBreakdown
    {
        public PriceModifier Modifier { get; }

        public decimal ApplicableBase { get; }

        public decimal TotalDelta { get; }

        private PriceModifierBreakdown() { }

        public PriceModifierBreakdown(PriceModifier modifier, decimal applicableBase, decimal totalDelta)
        {
            Modifier = modifier;
            ApplicableBase = applicableBase;
            TotalDelta = totalDelta;
        }

        public static PriceModifierBreakdown operator +(PriceModifierBreakdown lhs, PriceModifierBreakdown rhs)
        {
            Debug.Assert(lhs.Modifier == rhs.Modifier);

            return new PriceModifierBreakdown(lhs.Modifier,
                lhs.ApplicableBase + rhs.ApplicableBase,
                lhs.TotalDelta + rhs.TotalDelta);
        }

        public static PriceModifierBreakdown operator -(PriceModifierBreakdown lhs, PriceModifierBreakdown rhs)
        {
            Debug.Assert(lhs.Modifier == rhs.Modifier);

            return new PriceModifierBreakdown(lhs.Modifier,
                lhs.ApplicableBase - rhs.ApplicableBase,
                lhs.TotalDelta - rhs.TotalDelta);
        }
    }
}
