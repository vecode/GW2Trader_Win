using System.Collections.Generic;
using System.Linq;
using DataLayer.Db;
using DataLayer.Model;

namespace DataLayer.Repository
{
    public abstract class GenericRepository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IDatabaseProvider _dbProvider;

        protected GenericRepository(IDatabaseProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                return db.Table<T>();
            }
        }

        public virtual T Get(int id)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                return db.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public virtual int Save(T item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                if (item.Id != 0)
                {
                    db.Update(item);
                    return item.Id;
                }
                return db.Insert(item);
            }
        }

        public virtual int Delete(T item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                return db.Delete(item);
            }
        }
    }
}
