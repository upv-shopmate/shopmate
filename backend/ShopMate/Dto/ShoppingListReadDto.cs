#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class ShoppingListReadDto
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }

        public IReadOnlyCollection<ShoppingListEntryReadDto> Entries { get; }
    }
}
