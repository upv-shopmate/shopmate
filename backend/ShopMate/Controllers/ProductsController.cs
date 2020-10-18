using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public ProductsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{barcode}")]
        public ActionResult<ProductReadDto> GetProductByBarcode(string barcode)
        {
            var product = repository.Products.GetByBarcode(barcode);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductReadDto>(product));
        }
    }
}
