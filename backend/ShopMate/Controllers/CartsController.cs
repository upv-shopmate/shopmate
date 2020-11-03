using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Models;
using ShopMate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CartController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public CartController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("list")]
        public ActionResult<ShoppingListReadDto> GetCartContents()
        {
            var cart = repository.Carts.GetById(1);
            var contents = cart.Contents;

            return Ok(mapper.Map<ShoppingListReadDto>(contents));
        }

        [HttpPost("list")]
        public ActionResult<ShoppingListReadDto> AddContentsToCart([FromBody] ShoppingListEntryModifyDto dto)
        {
            var barcode = repository.Products.GetByBarcode(dto.ItemId);
            if (barcode is null)
            {
                return BadRequest("Unknown item ID.");
            }
            var entry = new ShoppingListEntry(dto.Quantity, barcode);

            var cart = repository.Carts.GetById(1); //Temporal hasta que haya loggin;
            cart.Contents.AddEntry(entry);
            repository.SaveChanges();

            return CreatedAtAction("GetCartContents", new object { });
        }

        [HttpDelete("list")]
        public ActionResult<ShoppingListReadDto> RemoveContentsFromCart([FromBody] ShoppingListEntryModifyDto dto)
        {
            var barcode = repository.Products.GetByBarcode(dto.ItemId);
            if (barcode is null)
            {
                return BadRequest("Unknown item ID.");
            }
            var entry = new ShoppingListEntry(dto.Quantity, barcode);

            var cart = repository.Carts.GetById(1); //Temporal hasta que haya loggin;
            cart.Contents.RemoveEntry(entry);

            return NoContent();
        }

        [HttpGet("tracking")]
        public ActionResult<ICollection<ShoppingListReadDto>> GetCurrentTrackingLists()
        {
            var cart = repository.Carts.GetById(1); //Temporal hasta que haya loggin
            ICollection<ShoppingList> shoppingLists = cart.TrackedLists;
            
            return Ok(shoppingLists);
        }

        [HttpPut("tracking")]
        public ActionResult TrackShoppingLists([FromBody] ICollection<int> ids)
        {
            ICollection<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in ids)
            {
                var shoppingList = repository.ShoppingLists.GetById(id);
                if (shoppingList is null) { return BadRequest("Unknown list ID."); }
                shoppingLists.Add(shoppingList);
            }

            var cart = repository.Carts.GetById(1); //Temporal hasta que haya loggin
            foreach (var shoppingList in shoppingLists)
            {
                if (!cart.TrackedLists.Contains(shoppingList))
                {
                    cart.TrackedLists.Add(shoppingList);
                }
            }

            return NoContent();
        }

        [HttpDelete("tracking")]
        public ActionResult UntrackShoppingLists([FromBody] ICollection<int> ids)
        {
            ICollection<ShoppingList> shoppingLists = new List<ShoppingList>();
            foreach (var id in ids)
            {
                var shoppingList = repository.ShoppingLists.GetById(id);
                if (shoppingList is null) { return BadRequest("Unknown list ID."); }
                shoppingLists.Add(shoppingList);
            }

            var cart = repository.Carts.GetById(1); //Temporal hasta que haya loggin
            foreach (var shoppingList in shoppingLists)
            {
                cart.TrackedLists.Remove(shoppingList);
            }

            return NoContent();
        }
    }
}
