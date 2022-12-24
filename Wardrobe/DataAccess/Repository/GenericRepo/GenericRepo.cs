using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected UserDbContext context;
        internal DbSet<T> dbSet;
        public GenericRepo(UserDbContext context)
        {
            this.context = context;

            dbSet = context.Set<T>();

        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public void Delete(int Id)
        {
            dbSet.Remove(dbSet.Find(Id));
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            // get all
            return await dbSet.ToListAsync();
        }


        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int Id)
        {
            return dbSet.Find(Id);

        }




    }
}
