using ShopMate.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ShopMate.Services
{
    public interface IShopMateAuthService
    {
        public bool FindUserByCredentials(string username, string password, out User user);

        public string LogIn(string audience, TimeSpan duration, params Claim[] claims);

        public string LogIn(User user)
            => LogIn("user-login", TimeSpan.FromHours(4),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "user"));

        public bool GetUserFromClaims(IEnumerable<Claim> claims, out User? user);
    }
}
