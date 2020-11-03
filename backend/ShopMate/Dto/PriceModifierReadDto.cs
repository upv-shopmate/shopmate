#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class PriceModifierReadDto
    {
        public string Code { get; internal set; }

        public string? Description { get; internal set; }

        public decimal Value { get; internal set; }

        public string Kind { get; internal set; }
    }
}
