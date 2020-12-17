#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class ShoppingListReadDto
    {
        public int Id { get; internal set; }

        public string Name { get; internal set; }

        public decimal SubtotalPrice { get; internal set; }

        public decimal TotalPrice { get; internal set; }

        public IReadOnlyCollection<ShoppingListEntryReadDto> Entries { get; internal set; }
    }
}
