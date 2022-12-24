using DataAccess.Context;
using DataAccess.DataModels;
using DataAccess.Repository.GenericRepo;

namespace DataAccess.Repository.ProductRepo
{
    public class ProductRepo : GenericRepo<Product>, IProductRepo
        {
        public ProductRepo(UserDbContext context) : base(context)
        {

        }
    }
}
