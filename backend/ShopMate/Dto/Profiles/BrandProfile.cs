using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandReadDto>();
        }
    }
}
