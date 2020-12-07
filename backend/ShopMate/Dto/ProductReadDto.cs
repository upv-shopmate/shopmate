#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class ProductReadDto
    {
        public int Id { get; internal set; }

        public string? Barcode { get; internal set; }

        public string Name { get; internal set; }

        public double? Weight { get; internal set; }

        public double? Volume { get; internal set; }

        public ushort? Units { get; internal set; }

        public string? OriginCountry { get; internal set; }

        public bool Edible { get; internal set; }

        public decimal Price { get; internal set; }

        public ICollection<string> Pictures { get; internal set; }

        public uint? AvailableStock { get; internal set; }

        public uint TimesSold { get; internal set; }

        public ICollection<BrandReadDto> Brands { get; internal set; }

        public ICollection<CategoryReadDto> Categories { get; internal set; }

        public ICollection<LabelReadDto> Labels { get; internal set; }

        public ICollection<PriceModifierReadDto> PriceModifiers { get; internal set; }

        // TODO
        //public ICollection<Position> Positions { get; internal set; }

        public ICollection<StoreReadDto> Vendors { get; internal set; }

        public decimal ModifiedPrice { get; internal set; }

        public decimal PriceWithVat { get; internal set; }
    }
}
