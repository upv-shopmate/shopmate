using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Models;
using ShopMate.Models.Interfaces;
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
                if (shoppingList is null) { return BadRequest(); }
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
            Console.WriteLine(repository.SaveChanges());
            return NoContent();
        }
    }
}
