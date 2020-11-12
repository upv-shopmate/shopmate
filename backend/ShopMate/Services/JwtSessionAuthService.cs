using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopMate.Models;
using ShopMate.Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ShopMate.Services
{
    public class JwtSessionAuthService : IShopMateAuthService
    {
        private readonly IConfiguration config;
        private readonly IShopMateRepository repository;

        public JwtSessionAuthService(IConfiguration config, IShopMateRepository repository)
        {
            this.config = config;
            this.repository = repository;
        }

        public bool FindUserByCredentials(string username, string password, out User user)
        {
            user = repository.Users.GetAll().FirstOrDefault(u => u.Email == username);
            if (user is null || user.Password != password)
            {
                return false;
            }

            return true;
        }

        public string LogIn(string audience, TimeSpan duration, params Claim[] claims)
        {
            var secret = Encoding.ASCII.GetBytes(config["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = config.GetSection("Jwt:Issuers").Get<List<string>>().First(),
                Audience = audience,
                Expires = DateTime.UtcNow + duration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}
