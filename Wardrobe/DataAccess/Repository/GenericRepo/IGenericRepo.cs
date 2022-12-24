using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repository.GenericRepo
{
    public interface IGenericRepo<T> where T : class
    {

        Task<IEnumerable<T>> GetAll();
        void Update(T entity);
        void Add(T entity);
        void Delete(T entity);
        void Delete(int Id);

        T Get(int Id);


    }
}
