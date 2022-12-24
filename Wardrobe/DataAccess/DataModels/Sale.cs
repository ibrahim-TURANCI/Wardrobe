using System;

namespace DataAccess.DataModels
{
    public class Sale
    {

        public int Id { get; set; }

        public int ProductId { get; set; }

        public string BuyerId { get; set; }

        public string SellerId { get; set; }

        public string Price { get; set; }

        public DateTime DateTime { get; private set; } = DateTime.UtcNow;


    }
}
