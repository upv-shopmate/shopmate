#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

namespace ShopMate.Dto
{
    public class ShoppingListEntryModifyDto
    {
        public int Quantity { get; set; }
        public string ItemId { get; set; }
    }
}
