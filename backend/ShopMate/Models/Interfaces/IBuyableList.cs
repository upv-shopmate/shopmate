using System.Collections.Generic;

namespace ShopMate.Models.Interfaces
{
    public interface IBuyableList<T>
        where T : IBuyable
    {
        public decimal SubtotalPrice { get; }
        public decimal TotalPrice { get; }

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns { get; }

        public void AddEntry(IBuyableListEntry<T> entry);

        public bool RemoveEntry(IBuyableListEntry<T> entry);
        public bool RemoveEntry(T item);
    }

    public interface IBuyableListEntry<T>
    {
        public int Quantity { get; internal set; }

        public T Item { get; }

        public decimal Price { get; }
        public decimal ModifiedPrice { get; }

        public IReadOnlyCollection<PriceModifierBreakdown> ModifierBreakdowns { get; }
    }

    public interface IBuyable {
        public decimal Price { get; }

        public decimal ModifiedPrice { get; }

        public IReadOnlyCollection<PriceModifier> PriceModifiers { get; }
    }
}
