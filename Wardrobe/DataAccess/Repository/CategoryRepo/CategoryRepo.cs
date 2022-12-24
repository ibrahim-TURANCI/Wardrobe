using DataAccess.Context;
using DataAccess.DataModels;
using DataAccess.Repository.GenericRepo;

namespace DataAccess.Repository.CategoryRepo
{
    public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(UserDbContext context) : base(context)
        {

        }
    }
}
