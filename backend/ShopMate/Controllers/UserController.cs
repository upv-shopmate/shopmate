using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Models;
using ShopMate.Persistence;
using ShopMate.Services;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;
        private readonly IShopMateAuthService auth;

        public UserController(IShopMateRepository repository, IMapper mapper, IShopMateAuthService auth)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.auth = auth;
        }

        [HttpGet("authorize")]
        public ActionResult<string> Authorize([FromQuery] string username, [FromQuery] string password) // FIXME
        {
            if (!auth.FindUserByCredentials(username, password, out User user))
            {
                return Unauthorized();
            }

            return auth.LogIn(user);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult<UserReadDto> GetCurrentUser()
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            return Ok(mapper.Map<UserReadDto>(user!));
        }

        [HttpGet("coupons")]
        [Authorize(Roles = "user")]
        public ActionResult<ICollection<CouponReadDto>> GetCurrentUserCoupons()
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            var coupons = user!.OwnedCoupons;
            return Ok(mapper.Map<List<CouponReadDto>>(coupons));
        }
    }
}
