using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>();
        }
    }
}
