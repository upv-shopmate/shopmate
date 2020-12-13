using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionReadDto>();
        }
    }
}
