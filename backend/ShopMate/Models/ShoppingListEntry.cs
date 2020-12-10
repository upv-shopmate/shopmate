#pragma warning disable CS8618 // El campo que acepta valores NULL está sin inicializar. Considere la posibilidad de declararlo como que acepta valores NULL.

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        public decimal ModifiedPrice
        {
            get
            {
                var @base = Quantity * Item.Price;

                return Item.PriceModifiers.Union(AdditionalModifiers)
                    .Aggregate(@base, (price, modifier) => modifier.Apply(price));
            }
        }

        [NotMapped]
        public List<PriceModifier> AdditionalModifiers { get; set; } = new List<PriceModifier>();

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns
        {
            get => Item.PriceModifiers.Select(modifier =>
                    new PriceModifierBreakdown(
                        modifier,
                        applicableBase: Quantity * Item.Price,
                        totalDelta: Quantity * modifier.DeltaFor(Item.Price)))
                    .ToList();
        }

        private ShoppingListEntry() { }

        public ShoppingListEntry(int quantity, Product product)
        {
            Quantity = quantity;
            Item = product;
        }
    }
}
