using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GW2Trader.ApiWrapper.Entities;
using GW2Trader.ApiWrapper.Wrapper;

namespace GW2TraderTest.Mock
{
    class ApiWrapperMock : ITradingPostApiWrapper
    {
        public IEnumerable<int> ItemIds()
        {
            return Enumerable.Range(0, 10);
        }

        public ApiItem ItemDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiItem> ItemDetails(IEnumerable<int> ids)
        {
            return ids.Select(x => new ApiItem { Id = x });
        }

        public ApiItemListing Listings(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiItemPrice> Prices(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
