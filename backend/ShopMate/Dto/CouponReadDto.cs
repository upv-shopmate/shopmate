#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class CouponReadDto : CouponReadShortDto
    {

        public ICollection<PriceModifierReadDto> Effects { get; internal set; }

        public ICollection<ProductReadDto> ApplicableProducts { get; internal set; }

        public StoreReadDto Store { get; internal set; }
    }
}
