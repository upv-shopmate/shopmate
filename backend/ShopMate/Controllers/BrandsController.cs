using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Linq;

namespace ShopMate.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BrandsController : ControllerBase
    {
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;

        public BrandsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<BrandReadDto> GetBrandById(int id)
        {
            var brand = repository.Brands.GetAll().FirstOrDefault(b => b.Id == id);

            if (brand is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BrandReadDto>(brand));
        }
    }
}
