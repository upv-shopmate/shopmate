using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class PriceModifierProfile : Profile
    {
        public PriceModifierProfile()
        {
            CreateMap<PriceModifier, PriceModifierReadDto>();
        }
    }
}
