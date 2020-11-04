using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class PriceModifierBreakdownProfile : Profile
    {
        public PriceModifierBreakdownProfile()
        {
            CreateMap<PriceModifierBreakdown, PriceModifierBreakdownReadDto>();
        }
    }
}
