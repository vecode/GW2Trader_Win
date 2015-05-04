using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GW2TPApiWrapper.Entities;
using System.IO;
using GW2TPApiWrapper.Util;

namespace GW2TPApiWrapper.Wrapper
{
    public class TradingPostApiWrapper : ITradingPostApiWrapper
    {
        private readonly IApiAccessor _apiAccessor;

        private const int MaxRequestSize = 200;

        private readonly Dictionary<Type, Func<int[], Stream>> _apiCalllingMethodDictionary;

        /// <summary>
        /// Number of ids per request (limited by API)
        /// </summary>
        private int _requestSize = MaxRequestSize;
        public int RequestSize
        {
            get { return _requestSize; }
            set
            {
                _requestSize = value > 1 ? Math.Min(MaxRequestSize, value) : _requestSize;
            }
        }

        public TradingPostApiWrapper(IApiAccessor apiAccessor)
        {
            _apiAccessor = apiAccessor;

            _apiCalllingMethodDictionary = new Dictionary<Type, Func<int[], Stream>>();
            _apiCalllingMethodDictionary[typeof(Item)] = _apiAccessor.ItemDetails;
            _apiCalllingMethodDictionary[typeof(ItemListing)] = _apiAccessor.Listings;
            _apiCalllingMethodDictionary[typeof(ItemPrice)] = _apiAccessor.Prices;
        }

        public IEnumerable<int> ItemIds()
        {
            Stream responseStream = _apiAccessor.ItemIds();
            int[] ids = ApiResponseConverter.DeserializeStream<int[]>(responseStream);
            return ids;
        }

        public Item ItemDetails(int id)
        {
            Stream responseStream = _apiAccessor.ItemDetails(id);
            if (responseStream == null) return null;

            Item item = ApiResponseConverter.DeserializeStream<Item>(responseStream);
            return item;
        }

        public ItemListing Listings(int id)
        {
            Stream responseStream = _apiAccessor.Listings(id);
            if (responseStream == null) return null;

            ItemListing listing = ApiResponseConverter.DeserializeStream<ItemListing>(responseStream);
            return listing;

        }

        public IEnumerable<Item> ItemDetails(IEnumerable<int> ids)
        {
            return ApiRequest<Item>(ids);
        }

        private IEnumerable<T> ApiRequest<T>(IEnumerable<int> ids) where T : GW2TPApiResponse
        {
            Func<int[], Stream> apiCallingMethod;

            if (!_apiCalllingMethodDictionary.TryGetValue(typeof(T), out apiCallingMethod))
            {
                throw new NotSupportedException("Type " + typeof(T) + " is not supported");
            }

            List<T> entites = new List<T>(ids.Count());
            int needRequests = (int)Math.Ceiling((double)ids.Count() / RequestSize);

            for (int i = 0; i < needRequests; i++)
            {
                var idSubset = ids.Skip(i * RequestSize).Take(RequestSize).ToArray();
                var responseStream = apiCallingMethod(idSubset);

                if (responseStream == null)
                {
                    break;
                }

                var entitySubset = ApiResponseConverter.DeserializeStream<T[]>(responseStream);
                entites.AddRange(entitySubset);
            }
            return entites;
        }

        public IEnumerable<ItemListing> Listings(IEnumerable<int> ids)
        {
            return ApiRequest<ItemListing>(ids);
        }

        public IEnumerable<ItemPrice> Prices(IEnumerable<int> ids)
        {
            return ApiRequest<ItemPrice>(ids);
        }
    }
}
