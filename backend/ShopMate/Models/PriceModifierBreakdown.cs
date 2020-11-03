#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System;
using System.Diagnostics;

namespace ShopMate.Models
{
    public class PriceModifierBreakdown
    {
        public int Id { get; private set; }

        public PriceModifier Modifier { get; }

        public decimal ApplicableBase { get; }

        public decimal TotalDelta { get; }

        PriceModifierBreakdown() { }

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
