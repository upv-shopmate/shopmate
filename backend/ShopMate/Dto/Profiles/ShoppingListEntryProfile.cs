using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class ShoppingListEntryProfile : Profile
    {
        public ShoppingListEntryProfile()
        {
            CreateMap<ShoppingListEntry, ShoppingListEntryReadDto>()
                .ForMember(dest => dest.TotalPrice, c => c.MapFrom(src => src.ModifiedPrice));
        }
    }
}