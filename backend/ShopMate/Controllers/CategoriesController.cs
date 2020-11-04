using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;
using System.Linq;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public CategoriesController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryReadDto> GetCategoryById(int id)
        {
            var category = repository.Categories.GetAll().FirstOrDefault(c => c.Id == id);

            if (category is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CategoryReadDto>(category));
        }
    }
}
