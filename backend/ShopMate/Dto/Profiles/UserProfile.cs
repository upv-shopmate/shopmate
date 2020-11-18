using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
