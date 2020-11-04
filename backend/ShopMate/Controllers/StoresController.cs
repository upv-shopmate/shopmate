using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Linq;

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
            var store = repository.Stores.GetAll().FirstOrDefault(s => s.Id == id);

            if (store is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<StoreReadDto>(store));
        }

        [HttpGet("{id}/map")]
        public ActionResult<int[][]> GetStoreMap(int id)
        {
            var store = repository.Stores.GetAll().FirstOrDefault(s => s.Id == id);

            if (store is null) 
            {
                return NotFound();
            }

            var map = store.Map;

            return Ok(map);
        }
    }
}
