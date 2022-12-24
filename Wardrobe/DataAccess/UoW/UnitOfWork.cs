using DataAccess.Context;
using DataAccess.Repository.CategoryRepo;
using DataAccess.Repository.OfferRepo;
using DataAccess.Repository.ProductRepo;

namespace DataAccess.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;

        public IProductRepo Products { get; private set; }
        public ICategoryRepo Categories { get; private set; }
        public IOfferRepo Offers { get; private set; }


        public UnitOfWork(UserDbContext context)
        {
            this._context = context;

            Products = new ProductRepo(_context);
            Categories = new CategoryRepo(_context);
            Offers = new OfferRepo(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }


    }
}
