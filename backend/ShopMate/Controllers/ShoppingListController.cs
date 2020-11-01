#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Models;
using ShopMate.Persistence;
using System.Linq;

namespace ShopMate.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ShoppingListsController : ControllerBase
    {

        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public ShoppingListsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<ShoppingListReadDto> GetShoppingListById(int id)
        {
            var list = repository.ShoppingLists.GetById(id);

            if (list is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ShoppingListReadDto>(list));
        }
    }
}
