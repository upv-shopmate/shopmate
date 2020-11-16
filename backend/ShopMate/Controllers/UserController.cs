using System.Collections.Generic;
using System.Linq;
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

        [HttpPost("authorize")]
        public ActionResult<object> Authorize([FromBody] UsernamePasswordPairAuthenticationDto credentials) // FIXME research standard way of doing this
        {
            if (!auth.FindUserByCredentials(credentials.Username, credentials.Password, out User user))
            {
                return Unauthorized();
            }

            return Ok(new { AccessToken = auth.LogIn(user) });
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

            var coupons = repository.Coupons.GetAll().Where(c => c.Owners.Contains(user));
            return Ok(mapper.Map<List<CouponReadDto>>(coupons));
        }
    }
}
