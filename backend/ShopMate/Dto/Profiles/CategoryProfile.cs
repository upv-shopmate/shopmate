using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryReadDto>();
        }
    }
}
