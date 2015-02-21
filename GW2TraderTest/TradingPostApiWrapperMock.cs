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
        private ApiTestDataFactory _apiDataFactory = new ApiTestDataFactory();

        public int[] ItemIds()
        {
            return _apiDataFactory
                .Items
                .Select(item => item.ID)
                .Distinct()
                .ToArray();
        }

        public ItemDetails ItemDetails(int id)
        {
            return _apiDataFactory
                .Items
                .Where(item => item.ID == id)
                .FirstOrDefault();
        }

        public ItemPrice ItemPrice(int id)
        {
            return _apiDataFactory
                .Prices
                .Where(price => price.Id == id)
                .FirstOrDefault();
        }

        public ItemListing Listings(int id)
        {
            return _apiDataFactory
                .ItemListings
                .Where(listing => listing.Id == id)
                .FirstOrDefault();              
        }
    }
}
