using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        int Save(T item);
        int Delete(T item);
    }
}
