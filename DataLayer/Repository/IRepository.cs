using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Return all entities.
        /// </summary>
        /// <returns>Return all entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Return entity by its id
        /// </summary>
        /// <param name="id">Id of the requested entity</param>
        /// <returns>Return entity by its id</returns>
        T Get(int id);

        /// <summary>
        /// Update or add given entity
        /// </summary>
        /// <param name="item">Entity to add or update</param>
        void Save(T item);

        /// <summary>
        /// Remove entity from repository
        /// </summary>
        /// <param name="item">Entity to remove</param>
        void Delete(T item);
    }
}
