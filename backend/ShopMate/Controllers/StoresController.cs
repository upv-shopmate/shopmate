using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public StoresController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<StoreReadDto> GetStoreById(int id)
        {
            var store = repository.Stores.GetById(id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StoreReadDto>(store));
        }
    }
}
