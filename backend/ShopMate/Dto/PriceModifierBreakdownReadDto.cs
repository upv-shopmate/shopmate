#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class PriceModifierBreakdownReadDto
    {
        public PriceModifierReadDto Modifier { get; internal set; }

        public decimal ApplicableBase { get; internal set; }

        public decimal TotalDelta { get; internal set; }
    }
}
