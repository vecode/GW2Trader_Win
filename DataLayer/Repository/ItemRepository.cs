using System.Collections.Generic;
using DataLayer.Db;
using DataLayer.Model;
using SQLiteNetExtensions.Extensions;
using System.Text;

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
                db.InsertOrReplaceAllWithChildren(items);
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

        public IEnumerable<Item> Search(
            string keyword, string rarity, string type, 
            string subType, int minLevel, int maxLevel, 
            int minMargin, int maxMargin, int minRoi,
            int maxRoi, int pageSize, int pageIndex)
        {
            List<object> parameters = new List<object>();

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(" SELECT *, ((SellPrice * 0.85)-BuyPrice) as Margin, ((SellPrice * 0.85)-BuyPrice)/(BuyPrice+0.00001) as ROI ");
            queryBuilder.Append(" FROM Item WHERE");

            if (!string.IsNullOrEmpty(keyword))
            {
                queryBuilder.Append(" Name LIKE ? AND ");               
                parameters.Add("%" + keyword + "%");
            }

            if (!string.IsNullOrEmpty(rarity))
            {
                queryBuilder.AppendLine(" Rarity LIKE ? AND");
                parameters.Add(rarity);
            }

            if (!string.IsNullOrEmpty(type))
            {
                queryBuilder.AppendLine(" Type LIKE ? AND ");
                parameters.Add(type);
            }

            if (!string.IsNullOrEmpty(subType))
            {
                queryBuilder.AppendLine(" SubType LIKE ? AND ");
                parameters.Add(subType);
            }

            queryBuilder.AppendLine(" Level >= ? AND Level <= ? AND ");
            parameters.Add(minLevel);
            parameters.Add(maxLevel);

            queryBuilder.AppendLine(" Margin >= ? AND Margin <= ? AND ");
            parameters.Add(minMargin);
            parameters.Add(maxMargin);

            queryBuilder.AppendLine(" ROI >= ? AND ROI <= ? ");
            parameters.Add(minRoi);
            parameters.Add(maxRoi);

            queryBuilder.Append(" LIMIT ?, ?; ");           

            parameters.Add(pageSize * pageIndex);
            parameters.Add(pageSize);

            using (var db = _dbProvider.GetDatabase())
            {
                return db.Query<Item>(queryBuilder.ToString(), parameters.ToArray());
            }
        }
    }
}
