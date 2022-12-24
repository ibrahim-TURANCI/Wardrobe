using DataAccess.Context;
using DataAccess.DataModels;
using DataAccess.Repository.GenericRepo;

namespace DataAccess.Repository.OfferRepo
{
    public class OfferRepo : GenericRepo<Offer>, IOfferRepo
    {
        public OfferRepo(UserDbContext context) : base(context)
        {

        }

    }
}
