namespace Entity.Product
{
    public class UpdateProduct
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        public int BrandId { get; set; }

        public string UsingStatus { get; set; }

        public decimal Price { get; set; }

        public string Explanation { get; set; }
    }
}
