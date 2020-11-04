using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreReadDto>();
        }
    }
}
