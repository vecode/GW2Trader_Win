using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Util;
using System.IO;

namespace GW2TPApiWrapper.Wrapper
{
    public class TradingPostApiWrapper : ITradingPostApiWrapper
    {
        private readonly IApiAccessor _apiAccessor;

        private const int MaxRequestSize = 200;

        /// <summary>
        /// Number of ids per request (limited by API)
        /// </summary>
        private int _requestSize = 200;
        public int RequestSize
        {
            get { return _requestSize; }
            set 
            {
                if (value < 1) return;
                else if (value > MaxRequestSize) _requestSize = MaxRequestSize;
                else _requestSize = value;
            }
        }

        public TradingPostApiWrapper(IApiAccessor apiAccessor)
        {
            _apiAccessor = apiAccessor;
        }

        public int[] ItemIds()
        {
            Stream responseStream = _apiAccessor.ItemIds();
            int[] ids =  ApiResponseConverter.DeserializeStream<int[]>(responseStream);
            return ids;
        }

        public Item ItemDetails(int id)
        {
            Stream responseStream = _apiAccessor.ItemDetails(id);
            if (responseStream == null) return null;
            else
            {
                Item item = ApiResponseConverter.DeserializeStream<Item>(responseStream);
                return item;
            }
        }

        public ItemListing Listings(int id)
        {
            Stream responseStream = _apiAccessor.Listings(id);
            if (responseStream == null) return null;
            else
            {                
                ItemListing listing = ApiResponseConverter.DeserializeStream<ItemListing>(responseStream);
                return listing;
            }
        }

        public List<Item> ItemDetails(int [] ids)
        {
            List<Item> items = new List<Item>(ids.Length);

            int neededRequests = (int)Math.Ceiling(ids.Length / RequestSize * 1.0f);

            int[] idSubset;
            Item[] itemSubset;
            Stream responseStream;
            for (int i = 0; i < neededRequests; i++)
            {
                idSubset = ids.Skip(i * RequestSize).Take(RequestSize).ToArray();
                responseStream = _apiAccessor.ItemDetails(idSubset);

                if (responseStream == null)
                    break;

                itemSubset = ApiResponseConverter.DeserializeStream<Item[]>(responseStream);
                items.AddRange(itemSubset);
            }
            return items;
        }

        public List<ItemListing> Listings(int[] ids)
        {
            Stream responseStream = _apiAccessor.Listings(ids);
            if (responseStream == null) return null;
            else
            {
                ItemListing[] listings = ApiResponseConverter.DeserializeStream<ItemListing[]>(responseStream);
                return listings.ToList();
            }
        }
    }
}
