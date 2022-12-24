namespace DataAccess.DataModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ColorId { get; set; }           

        public int CategoryId { get; set; }      

        public int SizeId { get; set; }           

        public int BrandId { get; set; }       

        public bool IsOfferable { get; set; }  

        public string UsingStatus { get; set; } 

        public decimal Price { get; set; }

        public string Explanation { get; set; }

        public string SellerId { get; set; }

        public bool IsSold { get; set; } = false;

    }
}
