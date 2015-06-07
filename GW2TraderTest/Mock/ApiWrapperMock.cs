using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2TraderTest.Mock
{
    class ApiWrapperMock : ITradingPostApiWrapper
    {
        public IEnumerable<int> ItemIds()
        {
            return Enumerable.Range(0, 10);
        }

        public GW2TPApiWrapper.Entities.ApiItem ItemDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItem> ItemDetails(IEnumerable<int> ids)
        {
            return ids.Select(x => new ApiItem { Id = x });
        }

        public GW2TPApiWrapper.Entities.ApiItemListing Listings(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItemListing> Listings(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GW2TPApiWrapper.Entities.ApiItemPrice> Prices(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
