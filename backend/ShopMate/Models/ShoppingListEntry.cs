#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.Models
{
    [Owned]
    public class ShoppingListEntry
    {
        public int Quantity { get; set; }

        public Product Item { get; set; }

        [Column(TypeName = "money")]
        public decimal Price => Quantity * Item.Price;

        [Column(TypeName = "money")]
        public decimal ModifiedPrice => Quantity * Item.ModifiedPrice;

        private ShoppingListEntry() { }

        public ShoppingListEntry(int quantity, Product product)
        {
            Quantity = quantity;
            Item = product;
        }
    }
}
