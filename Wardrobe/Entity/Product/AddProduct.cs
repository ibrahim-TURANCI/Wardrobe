using FluentValidation;

namespace Entity.Product
{
    public class AddProduct
    {

        public string Name { get; set; }

        public int ColorId { get; set; }

        public int CategoryId { get; set; }

        public int SizeId { get; set; }

        public int BrandId { get; set; }

        public bool IsOfferable { get; set; } = false;

        public string UsingStatus { get; set; }

        public decimal Price { get; set; }

        public string Explanation { get; set; }

        public string SellerId { get; set; }
    }
    public class AddProductValidotor : AbstractValidator<AddProduct>
    {
        public AddProductValidotor()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.ColorId).NotNull();
            RuleFor(x => x.CategoryId).NotNull();
            RuleFor(x => x.SizeId).NotNull();
            RuleFor(x => x.BrandId).NotNull();
            RuleFor(x => x.UsingStatus).NotNull();
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.Explanation).MaximumLength(500);
            RuleFor(x => x.Explanation).NotNull();


        }
    }
}
