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

        [HttpGet("lists")]
        [Authorize(Roles = "user")]
        public ActionResult<ICollection<ShoppingListReadDto>> GetCurrentUserShoppingLists()
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            var lists = repository.ShoppingLists.GetAll().Where(l => user! == l.Owner);
            return Ok(mapper.Map<List<ShoppingListReadDto>>(lists));
        }

        [HttpGet("lists/{id}", Name = "GetCurrentUserShoppingListById")]
        [Authorize(Roles = "user")]
        public ActionResult<ICollection<ShoppingListReadDto>> GetCurrentUserShoppingListById(int id)
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            var list = repository.ShoppingLists.GetAll().Where(l => l.Id == id && user! == l.Owner).FirstOrDefault();
            if (list is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<List<ShoppingListReadDto>>(list));
        }

        [HttpPost("lists")]
        [Authorize(Roles = "user")]
        public ActionResult CreateCurrentUserShoppingList([FromBody] ShoppingListCreateDto dto)
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            var list = new ShoppingList(dto.Name);
            list.Owner = user;

            foreach (var entry in dto.Entries)
            {
                if (!Gtin14.TryFromStandardBarcode(entry.ItemId, out Gtin14? barcode) || entry.Quantity < 1)
                {
                    return BadRequest();
                }
                var product = repository.Products.GetAll().FirstOrDefault(p => p.Barcode == barcode);    // FIXME
                if (product is null)
                {
                    return BadRequest();
                }

                list.AddEntry(new ShoppingListEntry(entry.Quantity, product));
            }

            repository.ShoppingLists.Add(list);
            repository.SaveChanges();

            return CreatedAtRoute("GetCurrentUserShoppingListById", new { list.Id });
        }

        [HttpDelete("lists/{id}")]
        [Authorize(Roles = "user")]
        public ActionResult DeleteUserShoppingListById(int id)
        {
            if (!auth.GetUserFromClaims(User.Claims, out User? user))
            {
                return Unauthorized();
            }

            var list = repository.ShoppingLists.GetAll().Where(l => l.Id == id && user! == l.Owner).FirstOrDefault();
            if (list is null)
            {
                return NotFound();
            }
            repository.ShoppingLists.Remove(list);
            repository.SaveChanges();

            return NoContent();
        }
    }
}
