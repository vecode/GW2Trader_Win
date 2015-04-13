using System;
using System.Collections.Generic;
using System.Linq;
using GW2Trader.Data;
using GW2Trader.DesignTimeErrorPrevention;
using GW2Trader.Model;
using GW2Trader.Service;
using GW2TraderTestxUnit.TestData;
using Xunit;

namespace GW2TraderTestxUnit.ServiceTest
{
    public class DataServiceTest
    {
        private readonly ITestDataFactory _testDataFactory;
        private readonly IGameDataContextProvider _contextProvider;

        public DataServiceTest()
        {
            _testDataFactory = new TestDataFactory();
            //_contextProvider = new FakeDataContextProvider(); 
        }

        [Fact]
        public void ItemShouldBeAdded()
        {
            GameItemModel item = _testDataFactory.GetTestGameItems().First();

            IDataService dataService = new DataService();

            dataService.Add(item);
            dataService.Save();

            GameItemModel fetchedItem = dataService.Items.SingleOrDefault(i => i.ItemId == item.ItemId);
            Assert.NotNull(fetchedItem);
        }

        [Fact]
        public void ItemShouldBeDeleted()
        {
            GameItemModel item = _testDataFactory.GetTestGameItems().First();

            IDataService dataService = new DataService();

            dataService.Add(item);
            dataService.Save();
            dataService.Delete(item);

            GameItemModel fetchedItem = dataService.Items.SingleOrDefault(i => i.ItemId == item.ItemId);
            Assert.Null(fetchedItem);
        }

        public void ItemShouldBeUpdated()
        {
            GameItemModel item = _testDataFactory.GetTestGameItems().First();

            IDataService dataService = new DataService();

            dataService.Add(item);
            dataService.Save();

            item.Name = "newName";
            dataService.Update(item);
            dataService.Save();

            GameItemModel fetchedItem = dataService.Items.SingleOrDefault(i => i.ItemId == item.ItemId);
            Assert.NotNull(fetchedItem);
            Assert.Equal("newName", fetchedItem.Name);
        }
    }
}
