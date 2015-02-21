using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GW2TPApiWrapper.Entities;

namespace GW2TPApiWrapper.Wrapper
{
    public class TradingPostApiWrapper : ITradingPostApiWrapper
    {

        private IApiAccessor _apiAccessor;

        public TradingPostApiWrapper(IApiAccessor apiAccessor)
        {
            _apiAccessor = apiAccessor;
        }

        public int[] ItemIds()
        {
            int[] ids = JsonConvert.DeserializeObject<int[]>(_apiAccessor.ItemIds());
            return ids;
        }

        public ItemDetails ItemDetails(int id)
        {
            String jsonResult = _apiAccessor.ItemDetails(id);
            if (String.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                ItemDetails item = JsonConvert.DeserializeObject<ItemDetails>(jsonResult);
                return item;
            }
        }

        public ItemPrice ItemPrice(int id)
        {
            String jsonResult = _apiAccessor.ItemPrice(id);
            if (String.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                ItemPrice itemPrice = JsonConvert.DeserializeObject<ItemPrice>(jsonResult);
                return itemPrice;
            }
        }

        public ItemListing Listings(int id)
        {
            String jsonResult = _apiAccessor.Listings(id);
            if (String.IsNullOrEmpty(jsonResult))
                return null;
            else
            {
                ItemListing itemListing = JsonConvert.DeserializeObject<ItemListing>(jsonResult);
                return itemListing;
            }
        }


    }
}
