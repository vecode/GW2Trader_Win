using System.Collections.Generic;
using System.Linq;
using DataLayer.Model;
using SQLiteNetExtensions.Extensions;
using GW2Trader.Data.Db;

namespace DataLayer.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IDatabaseProvider _dbProvider;

        protected Repository(IDatabaseProvider dbProvider)
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
                return db.GetWithChildren<T>(id);
            }
        }

        public virtual void Save(T item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                db.InsertOrReplaceWithChildren(item);
            }
        }

        public virtual void Delete(T item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                db.Delete(item);
            }
        }
    }
}
