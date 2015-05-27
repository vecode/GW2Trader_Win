using DataLayer.Db;
using DataLayer.Model;
using SQLiteNetExtensions.Extensions;

namespace DataLayer.Repository
{
    public class ItemRepository : GenericRepository<Item>
    {
        public ItemRepository(IDatabaseProvider dbProvider)
            : base(dbProvider) { }

        public override int Save(Item item)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                if (item.Icon != null)
                {
                    // database does not contain the icon
                    if (item.Icon.Id == 0)
                    {
                        item.Icon.Id = db.Insert(item.Icon);
                    }
                    db.InsertWithChildren(item);
                }

                if (db.Find<Item>(item.Id) != null)
                {
                    db.UpdateWithChildren(item);
                    return item.Id;
                }
                return db.Insert(item);
            }
        }

        public override Item Get(int id)
        {
            using (Database db = _dbProvider.GetDatabase())
            {
                Item item = db.Find<Item>(id);
                if (item != null)
                {
                    if (item.Icon != null && item.IconId != 0)
                    {
                        item.Icon = db.Find<Icon>(item.IconId);
                    }
                }
                return item;
            }
        }
    }
}
