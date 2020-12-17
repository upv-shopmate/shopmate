#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using System.Collections.Generic;

namespace ShopMate.Dto
{
    public class CouponReadShortDto
    {
        public string Name { get; internal set; }

        public string Code { get; internal set; }
    }
}
