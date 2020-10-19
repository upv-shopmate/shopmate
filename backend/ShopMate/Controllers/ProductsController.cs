using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        const int DEFAULT_ITEMS_PER_PAGE = 10;

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

        [HttpGet]
        public ActionResult<ICollection<ProductReadDto>> GetProductsPage([FromQuery] int page, [FromQuery] int itemsPerPage = DEFAULT_ITEMS_PER_PAGE)
        {
            var products = repository.Products.GetPage(page, itemsPerPage).ToList();

            if (!products.Any())
            {
                return NoContent();
            }

            return Ok(mapper.Map<List<ProductReadDto>>(products));
        }
    }
}
