using AutoMapper;
using DataAccess.DataModels;
using Entity.Category;
using Entity.Offer;
using Entity.Product;

namespace Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, AddProduct>();
            CreateMap<AddProduct, Product>();
            CreateMap<ShowProduct, Product>();
            CreateMap<Product, ShowProduct>();
            CreateMap<Product, UpdateProduct>();
            CreateMap<UpdateProduct, Product>();
            CreateMap<Category, AddCategory>();
            CreateMap<AddCategory, Category>();
            CreateMap<Offer, AddOffer>();
            CreateMap<AddOffer, Offer>();
        }

    }
}
