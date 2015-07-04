using System;
using System.Linq;

using SQLite.Net.Platform.Win32;
using SQLiteNetExtensions.Extensions;
using Xunit;

using GW2Trader.Data.Db;
using DataLayer.Model;

namespace DataLayerTest
{
    public class DatabaseTest
    {
        private const string DatabasePath = "TestDb.sqlite";

        private Database GetDatabase()
        {
            return new Database(new SQLitePlatformWin32(), DatabasePath);
        }

        private void DeleteDatabase()
        {
            if (System.IO.File.Exists(DatabasePath))
            {
                System.IO.File.Delete(DatabasePath);
            }
        }

        [Fact]
        public void InsertItemTest()
        {
            DeleteDatabase();

            Item item = TestDataProvider.GetItem();
            // add item to database
            using (Database db = GetDatabase())
            {
                db.Insert(item);
            }

            // test if database contains item
            using (Database db = GetDatabase())
            {
                Item dbItem = db.Table<Item>().First();

                Assert.Equal(item.Name, dbItem.Name);
                Assert.Equal(item.Rarity, dbItem.Rarity);
                Assert.Equal(item.Type, dbItem.Type);
                Assert.Equal(item.SubType, dbItem.SubType);
                Assert.Equal(item.Level, dbItem.Level);
                Assert.Equal(1, dbItem.Id);
            }
        }

        [Fact]
        public void InsertInvestmentTest()
        {
            DeleteDatabase();

            Item item = TestDataProvider.GetItem();
            Investment investment = TestDataProvider.GetInvestment();
            

            using (Database db = GetDatabase())
            {               
                db.Insert(item);
                db.InsertWithChildren(investment);
            }

            using (Database db = GetDatabase())
            {
                Investment dbInvestment = db.GetWithChildren<Investment>(1);

                Assert.Equal(investment.BuyPrice, dbInvestment.BuyPrice);
                Assert.Equal(investment.Count, dbInvestment.Count);
                Assert.Equal(investment.SellPrice, dbInvestment.SellPrice);
                Assert.Equal(investment.IsSold, dbInvestment.IsSold);

                Assert.NotNull(investment.Item);
            }
            
        }
    }
}
