using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Namotion.Reflection;
using Newtonsoft.Json;
using ShopMate.Dto;
using ShopMate.Persistence;
using System;
using System.Linq;

namespace ShopMate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        readonly IShopMateRepository repository;
        readonly IMapper mapper;

        public MapController(IShopMateRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<int[][]> GetMapByID(int id) 
        {
            var store = repository.Stores.GetById(id);

            var map = store.Map;

            return Ok(map);
        }

    }
}
