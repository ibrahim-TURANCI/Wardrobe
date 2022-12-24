using System;

namespace DataAccess.DataModels
{
    public class Offer
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int OfferPricePercent { get; set; }

        public string BuyerId { get; set; }

        public string SellerId { get; set; }

        public DateTime DateTime { get; private set; } = DateTime.UtcNow;

    }
}
