using AutoMapper;
using DataAccess.DataModels;
using DataAccess.UoW;
using Entity.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wardrobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allCategories = await unitOfWork.Categories.GetAll();
            var responseEntities = mapper.Map<IEnumerable<Category>, List<AddCategory>>(allCategories);
            return Ok(responseEntities);
        }

        [HttpGet]    //
        [Route("Products")]
        public async Task<IActionResult> GetProductsOfCategory([FromQuery] int Id)
        {

            var allContainers = await unitOfWork.Products.GetAll();
            return Ok(allContainers.Where(x => x.CategoryId == Id));

        }
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AddCategory request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var Category = mapper.Map<AddCategory, Category>(request);
            unitOfWork.Categories.Add(Category);
            unitOfWork.Complete();
            return Ok();
        }

        [HttpGet("id/{id}")]
        [Authorize]
        public IActionResult GetById([FromRoute] int id)
        {
            var Category = unitOfWork.Categories.Get(id);
            return Ok(Category);
        }

    }
}
