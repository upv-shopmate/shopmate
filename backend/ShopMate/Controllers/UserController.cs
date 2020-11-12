using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // FIXME this is a test, remove it later
        [HttpGet("test")]
        [Authorize]
        public ActionResult<int> Test()
        {
            var clientId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(clientId);
        }

        //Crear un get usuario actual con un Autorize
    }
}
