#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class PriceModifierReadDto
    {
        public int Id { get; private set; }

        public string Code { get; private set; }

        public string? Description { get; private set; }

        public decimal Value { get; private set; }

        public string Kind { get; private set; }
    }
}
