using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2Trader_WindowsTest.TestData;
using Newtonsoft.Json;

namespace GW2Trader_WindowsTest.Mock
{
    internal class TradingPostApiWrapperMock : ITradingPostApiWrapper
    {
        private readonly ItemPrice _priceToReturn;
        private readonly ItemListing _listingToReturn;

        public enum TestException
        {
            None,
            Web,
            Json
        };

        public TestException ExceptionToThrow { get; set; }

        public TradingPostApiWrapperMock(ItemPrice priceToReturn, ItemListing listingToReturn)
        {
            _priceToReturn = priceToReturn;
            _listingToReturn = listingToReturn;
            ExceptionToThrow = TestException.None;
        }

        public ItemListing Listings(int id)
        {
            CheckForException();
            return _listingToReturn;
        }

        public IEnumerable<ItemPrice> Prices(IEnumerable<int> ids)
        {
            CheckForException();

            List<ItemPrice> prices = new List<ItemPrice>(ids.Count());

            // add an ItemPrice foreach specified id
            prices.AddRange(ids.Select(id => new ItemPrice
            {
                Id = id,
                Buys = _priceToReturn.Buys,
                Sells = _priceToReturn.Sells
            }));
            return prices;
        }

        public IEnumerable<int> ItemIds()
        {
            throw new NotImplementedException();
        }

        public Item ItemDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> ItemDetails(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        private void CheckForException()
        {
            if (ExceptionToThrow == TestException.Json)
            {
                throw new JsonException();
            }

            if (ExceptionToThrow == TestException.Web)
            {
                throw new WebException();
            }
        }


        public IEnumerable<ItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
