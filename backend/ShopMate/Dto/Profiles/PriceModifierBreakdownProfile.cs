using AutoMapper;
using ShopMate.Models;
using System;

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
