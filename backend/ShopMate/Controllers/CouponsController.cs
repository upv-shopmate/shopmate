using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Linq;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;

        public CouponsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{code}")]
        public ActionResult<CouponReadDto> GetCouponByCode(string code)
        {
            var coupon = repository.Coupons.GetAll().FirstOrDefault(c => c.Code == code);

            if (coupon is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CouponReadDto>(coupon));
        }
    }
}
