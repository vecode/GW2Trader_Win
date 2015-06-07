using System.Collections.Generic;
using DataLayer.Db;
using DataLayer.Model;
using SQLiteNetExtensions.Extensions;

namespace DataLayer.Repository
{
    public class ItemRepository : Repository<Item>
    {
        public ItemRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }

        public override void Save(Item item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                db.InsertOrReplaceWithChildren(item);
            }
        }

        public void Save(IEnumerable<Item> items)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                db.InsertAll(items);
            }
        }

        public override Item Get(int id)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                return db.Find<Item>(id);
            }
        }

        public IEnumerable<Item> GetAll()
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                return db.GetAllWithChildren<Item>();
            }
        }
    }
}
