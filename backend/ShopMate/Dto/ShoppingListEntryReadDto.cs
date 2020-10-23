#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using ShopMate.Models;

namespace ShopMate.Dto
{
    public class ShoppingListEntryReadDto
    {
        public int Quantity { get; private set; }

        public ProductReadDto Product { get; private set; }
    }
}
