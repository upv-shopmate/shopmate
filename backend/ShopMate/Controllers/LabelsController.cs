using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Dto;
using ShopMate.Persistence;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public LabelsController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<LabelReadDto> GetLabelById(int id)
        {
            var label = repository.Labels.GetById(id);

            if (label is null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<LabelReadDto>(label));
        }
    }
}
