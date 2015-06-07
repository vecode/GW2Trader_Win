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
            _apiCalllingMethodDictionary[typeof(ApiItem)] = _apiAccessor.ItemDetails;
            _apiCalllingMethodDictionary[typeof(ApiItemListing)] = _apiAccessor.Listings;
            _apiCalllingMethodDictionary[typeof(ApiItemPrice)] = _apiAccessor.Prices;
        }

        public IEnumerable<int> ItemIds()
        {
            Stream responseStream = _apiAccessor.ItemIds();
            int[] ids = ApiResponseConverter.DeserializeStream<int[]>(responseStream);
            return ids;
        }

        public ApiItem ItemDetails(int id)
        {
            Stream responseStream = _apiAccessor.ItemDetails(id);
            if (responseStream == null) return null;

            ApiItem item = ApiResponseConverter.DeserializeStream<ApiItem>(responseStream);
            return item;
        }

        public ApiItemListing Listings(int id)
        {
            Stream responseStream = _apiAccessor.Listings(id);
            if (responseStream == null) return null;

            ApiItemListing listing = ApiResponseConverter.DeserializeStream<ApiItemListing>(responseStream);
            return listing;

        }

        public IEnumerable<ApiItem> ItemDetails(IEnumerable<int> ids)
        {
            return ApiRequest<ApiItem>(ids);
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

        public IEnumerable<ApiItemListing> Listings(IEnumerable<int> ids)
        {
            return ApiRequest<ApiItemListing>(ids);
        }

        public IEnumerable<ApiItemPrice> Prices(IEnumerable<int> ids)
        {
            return ApiRequest<ApiItemPrice>(ids);
        }
    }
}
