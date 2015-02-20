using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2Trader.Data;
using GW2Trader.Model;
using System.Collections.Generic;

namespace GW2TraderTest
{
    [TestClass]
    public class GameDataRepositoryTest
    {
        private GameDataRepository _dataRepository = new GameDataRepository(new GameDataContextMock());
        private TestGameDataFactory _testGameDataFactory = new TestGameDataFactory();

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
        public void WatchedItemIdsShouldBeAdded()
        {
            Assert.AreNotEqual(0, _dataRepository.ItemWatchlists.Count());
        }

        [TestMethod]
        public void InvestmentsShouldBeAdded()
        {
            Assert.AreNotEqual(0, _dataRepository.InvestmentLists);
        }

        [TestMethod]
        public void ItemShouldBeAdded()
        {
            int newItemId = 999;
            ItemIdWatchlistModel watchlist = _dataRepository.ItemWatchlists.First();
            _dataRepository.AddItemToWatchlist<int>(watchlist, newItemId);

            Assert.IsTrue(_dataRepository.ItemWatchlists.First().Items.Contains(newItemId));
        }

        [TestMethod]
        public void ItemShoudlBeRemoved()
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
        }
    }
}
