using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class LabelProfile : Profile
    {
        public LabelProfile()
        {
            CreateMap<Label, LabelReadDto>();
        }
    }
}
