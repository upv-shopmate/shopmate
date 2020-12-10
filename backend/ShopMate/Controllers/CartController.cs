using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Models;
using ShopMate.Persistence;
using ShopMate.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;
        private readonly IShopMateAuthService auth;

        public CartController(IShopMateRepository repository, IMapper mapper, IShopMateAuthService auth)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.auth = auth;
        }

        [HttpGet("list")]
        public ActionResult<ShoppingListReadDto> GetCartContents()
        {
            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1);
            var contents = cart.Contents;

            // FIXME
            contents.AddEntry(new ShoppingListEntry(3, repository.Products.GetAll().FirstOrDefault(p => p.Id == 1393)));
            contents.AddEntry(new ShoppingListEntry(1, repository.Products.GetAll().FirstOrDefault(p => p.Id == 2800)));

            return Ok(mapper.Map<ShoppingListReadDto>(contents));
        }

        [HttpPost("list")] //Mirar si hay que poner Put en vez de Post
        public ActionResult<ShoppingListReadDto> AddContentsToCart([FromBody] ShoppingListEntryModifyDto dto)
        {
            var barcode = repository.Products.GetAll().FirstOrDefault(p => p.Id == dto.ItemId);
            if (barcode is null)
            {
                return BadRequest("Unknown item ID.");
            }
            var entry = new ShoppingListEntry(dto.Quantity, barcode);

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1); //Temporal hasta que haya loggin;
            cart.Contents.AddEntry(entry);
            repository.SaveChanges();

            return CreatedAtAction("GetCartContents", new object { });
        }

        [HttpDelete("list")]
        public ActionResult<ShoppingListReadDto> RemoveContentsFromCart([FromBody] ShoppingListEntryModifyDto dto)
        {
            var barcode = repository.Products.GetAll().FirstOrDefault(p => p.Id == dto.ItemId);
            if (barcode is null)
            {
                return BadRequest("Unknown item ID.");
            }
            var entry = new ShoppingListEntry(dto.Quantity, barcode);

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1); //Temporal hasta que haya loggin;
            cart.Contents.RemoveEntry(entry);
            repository.SaveChanges();

            return NoContent();
        }

        [HttpGet("tracking")]
        public ActionResult<ICollection<ShoppingListReadDto>> GetCurrentTrackingLists()
        {
            var cart = repository.Carts.GetAll().FirstOrDefault(b => b.Id == 1); //Temporal hasta que haya loggin
            var shoppingLists = cart.TrackedLists;

            return Ok(mapper.Map<List<ShoppingListReadDto>>(shoppingLists));
        }

        [HttpPut("tracking")]
        public ActionResult TrackShoppingLists([FromBody] ICollection<int> ids)
        {
            ICollection<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in ids)
            {
                var shoppingList = repository.ShoppingLists.GetAll().FirstOrDefault(l => l.Id == id);
                if (shoppingList is null) { return BadRequest("Unknown list ID."); }
                shoppingLists.Add(shoppingList);
            }

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1); //Temporal hasta que haya loggin
            foreach (var shoppingList in shoppingLists)
            {
                if (!cart.TrackedLists.Contains(shoppingList))
                {
                    cart.TrackedLists.Add(shoppingList);
                }
            }
            repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("tracking")]
        public ActionResult UntrackShoppingLists([FromBody] ICollection<int> ids)
        {
            ICollection<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in ids)
            {
                var shoppingList = repository.ShoppingLists.GetAll().FirstOrDefault(l => l.Id == 1);
                if (shoppingList is null) { return BadRequest("Unknown list ID."); }
                shoppingLists.Add(shoppingList);
            }

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1); //Temporal hasta que haya loggin
            foreach (var shoppingList in shoppingLists)
            {
                cart.TrackedLists.Remove(shoppingList);
            }
            repository.SaveChanges();

            return NoContent();
        }

        [HttpGet("coupons")]
        public ActionResult<ICollection<CouponReadShortDto>> GetCouponsAppliedInCart()
        {
            Cart cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1); //Temporal hasta que haya loggin
            IReadOnlyCollection<Coupon> couponList = cart.AppliedCoupons;

            return Ok(mapper.Map<List<CouponReadShortDto>>(couponList));
        }

        [HttpPut("coupons")]
        public ActionResult ApplyCoupons([FromBody] ICollection<string> codes)
        {
            auth.GetUserFromClaims(User.Claims, out User? user);

            ICollection<Coupon> coupons = new List<Coupon>();
            foreach (var code in codes)
            {
                var coupon = repository.Coupons.GetAll().FirstOrDefault(c => c.Code == code);
                if (coupon is null) { return BadRequest("Unknown coupon ID."); }

                if (coupon.Owners.Any())
                {
                    if (user is null || !coupon.Owners.Contains(user))
                    {
                        return Unauthorized();
                    }
                }

                coupons.Add(coupon);
            }

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1);
            foreach (var coupon in coupons)
            {
                cart.ApplyCoupon(coupon);
            }
            repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("coupons")]
        public ActionResult UnapplyCoupons([FromBody] ICollection<string> codes)
        {
            ICollection<Coupon> coupons = new List<Coupon>();
            foreach (var code in codes)
            {
                var coupon = repository.Coupons.GetAll().FirstOrDefault(c => c.Code == code);
                if (coupon is null) { return BadRequest("Unknown coupon ID."); }

                coupons.Add(coupon);
            }

            var cart = repository.Carts.GetAll().FirstOrDefault(c => c.Id == 1);
            foreach (var coupon in coupons)
            {
                cart.UnapplyCoupon(coupon);
            }
            repository.SaveChanges();

            return NoContent();
        }
    }
}
