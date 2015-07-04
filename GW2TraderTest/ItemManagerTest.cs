using System.Collections.Generic;
using System.Linq;
using DataLayer.Repository;
using GW2Trader.ApiWrapper.Wrapper;
using GW2Trader.Manager;
using GW2TraderTest.Mock;
using Xunit;

namespace GW2TraderTest
{
    public class ItemManagerTest
    {
        ItemManager GetManager()
        {
            return new ItemManager(GetRepository(), new ApiWrapperMock());
        }

        ItemRepository GetRepository()
        {
            return new ItemRepository(new TestDatabaseProvider());
        }

        [Fact]
        public void MissingItemIdsTest()
        {
            ItemManager itemManager = GetManager();
            List<int> missingItems = itemManager.MissingItemIds();

            Assert.Equal(new ApiWrapperMock().ItemIds(), missingItems);
        }

        [Fact]
        public void BuildDbTest()
        {
            ITradingPostApiWrapper _apiWrapper;

            ItemManager itemManager = GetManager();
            itemManager.BuildItemDb();

            ItemRepository repository = GetRepository();
            List<int> itemIds = repository.GetAll().Select(x => x.Id).ToList();

            Assert.Equal(new ApiWrapperMock().ItemIds(), itemIds);
        }
    }    
}
