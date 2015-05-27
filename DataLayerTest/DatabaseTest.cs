using DataLayer.Db;
using DataLayer.Model;
using SQLite.Net.Platform.Win32;
using SQLiteNetExtensions.Extensions;
using Xunit;

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
        public void InsertIconTest()
        {
            DeleteDatabase();

            // add icon to database
            Icon icon = TestDataProvider.GetIcon();
            using (Database db = GetDatabase())
            {
                db.Insert(icon);
            }

            // test if database contains icon
            using (Database db = GetDatabase())
            {
                Icon dbIcon = db.Table<Icon>().First();

                Assert.Equal(icon.FilePath, dbIcon.FilePath);
                Assert.Equal(icon.Url, dbIcon.Url);
                Assert.Equal(1, dbIcon.Id);
            }
        }

        [Fact]
        public void AddIconToItemTest()
        {
            DeleteDatabase();

            Item item = TestDataProvider.GetItem();
            Icon icon = TestDataProvider.GetIcon();

            item.Icon = icon;

            using (Database db = GetDatabase())
            {
                db.Insert(icon);
                db.InsertWithChildren(item);
            }

            using (Database db = GetDatabase())
            {
                Item dbItem = db.GetWithChildren<Item>(item.Id);

                Assert.Equal(item.Icon.FilePath, dbItem.Icon.FilePath);
                Assert.Equal(item.Icon.Url, dbItem.Icon.Url);
            }
        }
    }

}
