using DataAccess.Repository.CategoryRepo;
using DataAccess.Repository.OfferRepo;
using DataAccess.Repository.ProductRepo;

namespace DataAccess.UoW
{
    public interface IUnitOfWork
    {

        IProductRepo Products { get; }
        ICategoryRepo Categories { get; }
        IOfferRepo Offers { get; }

        int Complete();
    }
}
