using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GW2Trader.Model;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;

namespace GW2TraderTest
{
    [TestClass]
    public class GameItemModelTest
    {
        private GameItemModel _gameItem;

        public GameItemModelTest()
        {
            ItemPrice price = new ItemPrice
            {
                Buys = new Listing { Quantity = 10, UnitPrice = 100 },
                Sells = new Listing { Quantity = 20, UnitPrice = 150}
            };
            _gameItem = new GameItemModel { Price = price };
        }

        [TestMethod]
        public void MarginTest()
        {
            int expectedMargin = 28;
            Assert.AreEqual(expectedMargin, _gameItem.Margin);
        }

        [TestMethod]
        public void ROITest()
        {
            int expectedROI = 28;
            Assert.AreEqual(expectedROI, _gameItem.ROI());
        }
    }
}
