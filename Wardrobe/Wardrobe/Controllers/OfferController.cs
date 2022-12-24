using AutoMapper;
using DataAccess.DataModels;
using DataAccess.UoW;
using Entity.Identity;
using Entity.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wardrobe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OfferController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> _userManager)
        {
            this._userManager = _userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()               //Şuanki kullanıcının offerlarını listeler.
        {
            var User = await GetCurrentUserAsync();
            var allOffers = await unitOfWork.Offers.GetAll();
            var responseEntities = mapper.Map<IEnumerable<Offer>, List<AddOffer>>(allOffers.Where(x => x.BuyerId == User.Id.ToString()));
            return Ok(responseEntities);
        }

        [HttpGet("Product")]
        [Authorize]
        public async  Task<IActionResult> GetProductOffers()
        {

            var User = await GetCurrentUserAsync();
            var allOffers = await unitOfWork.Offers.GetAll();
            var responseEntities = mapper.Map<IEnumerable<Offer>, List<AddOffer>>(allOffers.Where(x => x.SellerId == User.Id.ToString()));
            return Ok(responseEntities);
        }


        [HttpGet("Approve/{id}")]
        [Authorize]
        public async Task<IActionResult> ApproveOffer([FromRoute] int id)
        {

            var Seller = await GetCurrentUserAsync();
            var offer =  unitOfWork.Offers.Get(id);
           var product = unitOfWork.Products.Get(offer.ProductId);
            product.IsSold = true;
            unitOfWork.Complete();
            return Ok(offer);
        }

        [HttpGet("Decline/{id}")]
        [Authorize]
        public async Task<IActionResult> DeclineOffer([FromRoute] int id)
        {

            var Seller = await GetCurrentUserAsync();
            var offer = unitOfWork.Offers.Get(id);
            unitOfWork.Offers.Delete(id);
            unitOfWork.Complete();
            return Ok(offer);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AddOffer request)
        {


            if (request == null)
            {
                return BadRequest();
            }

            if (request.Price != 0 && request.OfferPricePercent != 0)
            {
                return BadRequest("Price ve Percent aynı anda girilemez");
            }


            var product = unitOfWork.Products.Get(request.ProductId);
            if (!product.IsOfferable)
            {
                return BadRequest("This product is not offerable");
            }
            var Offer = mapper.Map<AddOffer, Offer>(request);
            var User = GetCurrentUserAsync();                                //işlemi yapan userin Idsi
            if (request.OfferPricePercent != 0)
            {
                Offer.Price = product.Price * request.OfferPricePercent / 100;
            }
            Offer.BuyerId = User.Result.Id;
            Offer.SellerId = product.SellerId;                          //Teklif verilen ürünü ekleyen User in Idsi 
            unitOfWork.Offers.Add(Offer);
            unitOfWork.Complete();
            return Ok();
        }


        private async Task<ApplicationUser> GetCurrentUserAsync() //bağlı kullanıcı
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete([FromRoute] int id)
        {

            var Offer = unitOfWork.Offers.Get(id);
            if (Offer == null)
            {
                return BadRequest("Offer not Found");
            }
            unitOfWork.Offers.Delete(id);
            unitOfWork.Complete();
            return Ok(id);
        }

    }
}
