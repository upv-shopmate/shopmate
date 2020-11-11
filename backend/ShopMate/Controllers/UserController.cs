using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShopMate.Persistence;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;
        private readonly IConfiguration config;

        public UserController(IShopMateRepository repository, IMapper mapper, IConfiguration config)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.config = config;
        }

        // TODO accept user credentials
        [HttpGet("authorize")]
        public ActionResult<string> Authorize()
        {
            var secret = Encoding.ASCII.GetBytes(config["Jwt:Secret"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "customer")
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = config.GetSection("Jwt:Issuers").Get<List<string>>().First(),
                Audience = "user-login",
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(tokenHandler.WriteToken(createdToken));
        }

        // FIXME this is a test, remove it later
        [HttpGet("test")]
        [Authorize]
        public ActionResult<int> Test()
        {
            var clientId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(clientId);
        }
    }
}
