#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class ProductReadDto
    {
        public string Barcode { get; private set; }

        public string Name { get; private set; }

        public double? Weight { get; private set; }

        public double? Volume { get; private set; }

        public ushort? Units { get; private set; }

        public string? OriginCountry { get; private set; }

        public bool Edible { get; private set; }

        public decimal Price { get; private set; }

        public ICollection<string> Pictures { get; private set; }

        public uint? AvailableStock { get; private set; }

        public uint TimesSold { get; private set; }

        public ICollection<BrandReadDto> Brands { get; private set; }

        public ICollection<CategoryReadDto> Categories { get; private set; }

        public ICollection<LabelReadDto> Labels { get; private set; }

        public ICollection<PriceModifierReadDto> PriceModifiers { get; private set; }

        // TODO
        //public ICollection<Position> Positions { get; private set; }

        public ICollection<StoreReadDto> Vendors { get; private set; }

        public decimal ModifiedPrice { get; private set; }

        public decimal PriceWithVat { get; private set; }
    }
}
