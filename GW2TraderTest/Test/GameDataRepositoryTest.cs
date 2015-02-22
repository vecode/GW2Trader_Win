using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2Trader.Data;
using GW2Trader.Model;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapperTest;
using System;
using System.Data.Entity.Validation;
using System.Text;

namespace GW2TraderTest
{
    [TestClass]
    public class GameDataRepositoryTest
    {
        private GameDataRepository _dataRepository = new GameDataRepository(new GameDataContextMock());
        private TestGameDataFactory _testGameDataFactory = new TestGameDataFactory();


        #region constructor thest
        [TestMethod]
        public void ItemsFromContextShouldBeAddedToDictionary()
        {
            // check whether repository has added game items from context
            Assert.AreEqual(3, _dataRepository.GetAllGameItems().Count());
            Assert.IsNotNull(_dataRepository.GameItemById(123456));
            Assert.IsNotNull(_dataRepository.GameItemById(2));
            Assert.IsNotNull(_dataRepository.GameItemById(3));

            // this should not be added
            Assert.IsNull(_dataRepository.GameItemById(99));
        }


        [TestMethod]
        public void ItemWatchlistsShouldBeAdded()
        {
            Assert.AreNotEqual(0, _dataRepository.ItemWatchlists.Count);
        }

        [TestMethod]
        public void InvestmentsShouldBeAdded()
        {
            Assert.AreNotEqual(0, _dataRepository.InvestmentLists);
        }
        #endregion

        [TestMethod]
        public void ItemShouldBeAdded()
        {
            GameItemModel item = new GameItemModel { Id = 999 };
            ItemWatchlistModel watchlist = _dataRepository.ItemWatchlists.First();
            _dataRepository.AddItemToWatchlist(watchlist, item);

            Assert.IsTrue(_dataRepository.ItemWatchlists.First().Items.Contains(item));
        }

        [TestMethod]
        public void ItemShouldBeRemoved()
        {
            InvestmentModel validInvestment = new InvestmentModel
            {
                ItemId = 2,
                Count = 5,
                PurchasePrice = 12345,
                DesiredSellPrice = 99999,
                IsSold = false,
                SoldFor = null
            };
            InvestmentWatchlistModel investments = _dataRepository.InvestmentLists.First();
            _dataRepository.DeleteItemFromWatchlist(investments, validInvestment);
            Assert.IsFalse(_dataRepository.InvestmentLists.First().Items.Contains(validInvestment));
        }

        [TestMethod]
        public void DbShouldBeBuild()
        {
            GameDataRepository dataRepository = new GameDataRepository(new GameDataContextMock());
            ITradingPostApiWrapper wrapper = new TradingPostApiWrapperMock();

            dataRepository.RebuiltGameItemDatabase(wrapper);

            ApiTestDataFactory apiDataFactory = new ApiTestDataFactory();
            int[] ids = apiDataFactory.Items.Select(item => item.ID).ToArray();
            Array.ForEach( ids, id => Assert.IsNotNull( dataRepository.GameItemById(id) ) );
        }

        [TestMethod]
        public void InvestmentShouldBeAdded()
        {
            GameItemModel item = _testGameDataFactory.GetTestGameItems().First();
            InvestmentModel investment = new InvestmentModel
            {
                Count = 20,
                DesiredSellPrice = 10000,
                GameItem = item,
                IsSold = false,
                PurchasePrice = 500
            };
           
            InvestmentWatchlistModel investmentWatchlist = _dataRepository.InvestmentLists.First();
            int investmentCount = investmentWatchlist.Items.Count;

            investmentWatchlist.Items.Add(investment);

            Assert.AreEqual(investmentCount + 1, investmentWatchlist.Items.Count);
        }
    }
}
