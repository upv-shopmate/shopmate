using AutoMapper;
using ShopMate.Models;

namespace ShopMate.Dto.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponReadShortDto>();
            CreateMap<Coupon, CouponReadDto>();
        }
    }
}
