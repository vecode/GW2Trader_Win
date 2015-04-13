using System;
using System.Collections.Generic;
using System.Linq;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapperTest;

namespace GW2TraderTest.Mock
{
    public class TradingPostApiWrapperMock : ITradingPostApiWrapper
    {
        private readonly ApiTestDataFactory _testDataFactory;

        public TradingPostApiWrapperMock(ApiTestDataFactory testDataFactory)
        {
            _testDataFactory = testDataFactory;
        }

        public IEnumerable<int> ItemIds()
        {
            return _testDataFactory
                .Items
                .Select(item => item.Id)
                .Distinct()
                .ToArray();
        }

        public Item ItemDetails(int id)
        {
            return _testDataFactory
                .Items.FirstOrDefault(item => item.Id == id);
        }

        public ItemPrice ItemPrice(int id)
        {
            throw new NotImplementedException();
            //return _testDataFactory
            //    .Prices
            //    .Where(price => price.Id == id)
            //    .FirstOrDefault();
        }

        public ItemListing Listings(int id)
        {
            return _testDataFactory
                .ItemListings.FirstOrDefault(listing => listing.Id == id);              
        }

        public IEnumerable<Item> ItemDetails(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemPrice> Prices(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public ItemPrice Price(int id)
        {
            throw new NotImplementedException();
        }
    }
}
