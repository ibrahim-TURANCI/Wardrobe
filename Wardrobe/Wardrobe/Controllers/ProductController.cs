using AutoMapper;
using DataAccess.DataModels;
using DataAccess.UoW;
using Entity.Identity;
using Entity.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Wardrobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allProducts = await unitOfWork.Products.GetAll();
            var responseEntities = mapper.Map<IEnumerable<Product>, List<AddProduct>>(allProducts);
            return Ok(responseEntities);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] AddProduct request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var product = mapper.Map<AddProduct, Product>(request);
            var SellerId = GetCurrentUserAsync();
            product.SellerId = SellerId.Result.Id;
            unitOfWork.Products.Add(product);
            unitOfWork.Complete();
            return Ok();
        }

        [HttpGet("Sell/{id}")]
        [Authorize]
        public IActionResult SellProduct([FromRoute] int id)
        {

            var product = unitOfWork.Products.Get(id);
            product.IsSold = true;
            unitOfWork.Complete();
            return Ok(product);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] UpdateProduct request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            Product oldproduct = unitOfWork.Products.Get(request.Id);
            oldproduct.Id = request.Id;
            oldproduct.Name = request.Name;
            oldproduct.Explanation = request.Explanation;
            oldproduct.ColorId = request.ColorId;
            oldproduct.SizeId = request.SizeId;
            oldproduct.BrandId = request.BrandId;
            oldproduct.UsingStatus = request.UsingStatus;
            oldproduct.Price = request.Price;

            unitOfWork.Products.Update(oldproduct);
            unitOfWork.Complete();
            return Ok();
        }



        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] int id)
        {


            unitOfWork.Products.Delete(id);
            unitOfWork.Complete();
            return Ok(id);
        }


        private async Task<ApplicationUser> GetCurrentUserAsync() //bağlı kullanıcı
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }


    }
}
