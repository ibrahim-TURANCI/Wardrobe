using DataAccess.DataModels;
using Entity.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public interface IUserDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Offer> Offers { get; set; }
    }
}
