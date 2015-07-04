using System;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader.Desktop.Model;
using GW2Trader.Desktop.Service;
using Xunit;
using GW2Trader_WindowsTest.Mock;
using GW2Trader_WindowsTest.TestData;

namespace GW2Trader_WindowsTest.ServiceTest
{
    public class CommerceServiceTest
    {
        private readonly ItemPrice _updatedPrice;
        private readonly ItemListing _updatedListing;
        private readonly TradingPostApiWrapperMock _tpWrapper;
        private readonly ITestDataFactory _testDataFactory;

        public CommerceServiceTest()
        {
            _updatedPrice = new ItemPrice
            {
                Buys = new Listing {Quantity = 100, UnitPrice = 125},
                Sells = new Listing {Quantity = 200, UnitPrice = 250}
            };

            _updatedListing = new ItemListing
            {
                Buys = new Listing[] {new Listing {Quantity = 100, UnitPrice = 125}},
                Sells = new Listing[] {new Listing {Quantity = 200, UnitPrice = 250}}
            };

            _tpWrapper = new TradingPostApiWrapperMock(_updatedPrice, _updatedListing);
            _testDataFactory = new TestDataFactory();
        }

        [Fact]
        public void PricesShouldBeUpdated()
        {
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.None;
            NotifyServiceMock notifyService = new NotifyServiceMock();
            ICommerceService commerceService = new CommerceService(_tpWrapper, notifyService);

            List<GameItemModel> itemsToUpdate = _testDataFactory.GetTestGameItems().ToList();

            commerceService.UpdatePrices(itemsToUpdate);
            foreach (GameItemModel item in itemsToUpdate)
            {
                Assert.Equal(_updatedPrice.Sells.UnitPrice, item.SellPrice);
                Assert.Equal(_updatedPrice.Sells.Quantity, item.SellListingQuantity);
                Assert.Equal(_updatedPrice.Buys.UnitPrice, item.BuyPrice);
                Assert.Equal(_updatedPrice.Buys.Quantity, item.BuyOrderQuantity);
            }
        }

        [Fact]
        public void UpdatingPricesShouldNotifyOnExceptions()
        {
            NotifyServiceMock notifyService = new NotifyServiceMock();
            ICommerceService commerceService = new CommerceService(_tpWrapper, notifyService);

            List<GameItemModel> itemsToUpdate = _testDataFactory.GetTestGameItems().ToList();

            // make sure notifyService was not notified before throwing an exception
            Assert.False(notifyService.IsNotified);

            // WebException
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.Web;
            commerceService.UpdatePrices(itemsToUpdate);
            Assert.True(notifyService.IsNotified);

            // JsonException
            notifyService.IsNotified = false;
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.Json;
            commerceService.UpdatePrices(itemsToUpdate);
            Assert.True(notifyService.IsNotified);
        }

        [Fact]
        public void ListingsShouldBeUpdated()
        {
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.None;
            NotifyServiceMock notifyService = new NotifyServiceMock();
            ICommerceService commerceService = new CommerceService(_tpWrapper, notifyService);

            GameItemModel itemToUpdate = _testDataFactory.GetTestGameItems().First();
            commerceService.UpdateListings(itemToUpdate);

            Assert.Equal(_updatedListing.Buys, itemToUpdate.Listing.Buys);
            Assert.Equal(_updatedListing.Sells, itemToUpdate.Listing.Sells);
        }

        [Fact]
        public void UpdatingListingsShouldNotifyOnExceptions()
        {
            NotifyServiceMock notifyService = new NotifyServiceMock();
            ICommerceService commerceService = new CommerceService(_tpWrapper, notifyService);

            GameItemModel itemToUpdate = _testDataFactory.GetTestGameItems().First();

            // make sure notifyService was not notified before throwing an exception
            Assert.False(notifyService.IsNotified);

            // WebException
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.Web;
            commerceService.UpdateListings(itemToUpdate);
            Assert.True(notifyService.IsNotified);

            // JsonException
            notifyService.IsNotified = false;
            _tpWrapper.ExceptionToThrow = TradingPostApiWrapperMock.TestException.Json;
            commerceService.UpdateListings(itemToUpdate);
            Assert.True(notifyService.IsNotified);
        }
    }
}
    