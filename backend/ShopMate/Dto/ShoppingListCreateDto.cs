#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class ShoppingListCreateDto
    {
        public string Name { get; set; }

        public ICollection<ShoppingListEntryModifyDto> Entries { get; set; }
    }
}
