using FluentValidation;

namespace Entity.Offer
{
    public class AddOffer
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int OfferPricePercent { get; set; }

        public string BuyerId { get; set; }

        public string SellerId { get; set; }

    }

    public class AddOfferValidotor : AbstractValidator<AddOffer>
    {
        public AddOfferValidotor()
        {


        }
    }

}
