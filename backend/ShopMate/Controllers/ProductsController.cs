using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private const int DEFAULT_ITEMS_PER_PAGE = 10;
        private readonly IShopMateRepository repository;
        private readonly IMapper mapper;

        public ProductsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<ProductReadDto> GetProductById(int id)
        {
            var product = repository.Products.GetAll().FirstOrDefault(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<ProductReadDto>(product));
        }

        [HttpGet("barcode/{barcode}")]
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
        public ActionResult<PageReadDto<ProductReadDto>> GetProductsPage([FromQuery] int page, [FromQuery] int itemsPerPage = DEFAULT_ITEMS_PER_PAGE)
        {
            var products = repository.Products.GetPage(page, itemsPerPage, out var hasNextPage).ToList();

            if (!products.Any())
            {
                return NoContent();
            }

            return Ok(new PageReadDto<ProductReadDto>(
                mapper.Map<List<ProductReadDto>>(products),
                nextPage: hasNextPage ? (int?)page + 1 : null));
        }

        [HttpGet("search")]
        public ActionResult<PageReadDto<ProductReadDto>> SearchProducts([FromQuery] string query, [FromQuery] int page, [FromQuery] int itemsPerPage = DEFAULT_ITEMS_PER_PAGE)
        {

            var products = repository.Products.SearchByQuery(query, page, itemsPerPage, out var hasNextPage);

            return Ok(new PageReadDto<ProductReadDto>(
                mapper.Map<List<ProductReadDto>>(products),
                nextPage: hasNextPage ? (int?)page + 1 : null));
        }
    }
}
