using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;

namespace ShopMate.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BrandsController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public BrandsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<BrandReadDto> GetBrandById(int id)
        {
            var brand = repository.Brands.GetById(id);

            if (brand is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BrandReadDto>(brand));
        }
    }
}
