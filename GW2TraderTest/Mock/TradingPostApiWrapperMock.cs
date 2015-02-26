using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Entities;
using GW2TraderTest;
using GW2TPApiWrapperTest;

namespace GW2TraderTest
{
    public class TradingPostApiWrapperMock : ITradingPostApiWrapper
    {
        private ApiTestDataFactory _testDataFactory;

        public TradingPostApiWrapperMock(ApiTestDataFactory testDataFactory)
        {
            _testDataFactory = testDataFactory;
        }

        public int[] ItemIds()
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
                .Items
                .Where(item => item.Id == id)
                .FirstOrDefault();
        }

        // TODO obsolete
        //public ItemPrice ItemPrice(int id)
        //{
        //    return _apiDataFactory
        //        .Prices
        //        .Where(price => price.Id == id)
        //        .FirstOrDefault();
        //}

        public ItemListing Listings(int id)
        {
            return _testDataFactory
                .ItemListings
                .Where(listing => listing.Id == id)
                .FirstOrDefault();              
        }


        public List<Item> ItemDetails(int[] ids)
        {
            throw new NotImplementedException();
        }


        public List<ItemListing> Listings(int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
